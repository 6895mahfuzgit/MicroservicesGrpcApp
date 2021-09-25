using AutoMapper;
using CommandService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"Called GRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                Console.WriteLine("Client Worked!!!!!!!!!");
                List<Platform> result = new List<Platform>();
                if (reply.Platform.Any())
                {
                    foreach (var item in reply.Platform)
                    {
                        Console.WriteLine(item.Name+"    "+ item.PlatformId);
                        result.Add(new Platform
                        {
                            ExternalID=item.PlatformId,
                            Name=item.Name, 
                        });
                    }
                }
                return result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't Call GRPC Server. Error: {ex.Message}");
                return Enumerable.Empty<Platform>();
            }
        }
    }
}

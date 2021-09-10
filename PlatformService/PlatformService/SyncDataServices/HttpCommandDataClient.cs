using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace PlatformService.SyncDataServices
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platformRead)
        {
            try
            {
                var httpContext = new StringContent(
                    JsonSerializer.Serialize(platformRead),
                    Encoding.UTF8,
                    "application/json"
                    );

                var response = await _httpClient.PostAsync(_configuration["CommandServiceURI"], httpContext);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("sync Post Respone is Ok");
                }
                else
                {

                    Console.WriteLine("sync Post Respone is Not OK");
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

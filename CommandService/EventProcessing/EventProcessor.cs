using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Enums;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        public void Process(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                case EventType.Undetermined:
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificatiobMsg)
        {
            Console.WriteLine(" DetermineEvent Start");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificatiobMsg);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform_Published Detcted in Enum Switch");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("Can't Determine Event Type.");
                    return EventType.Undetermined;
            }
        }


        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetService<ICommandRepo>();
                var platfromPublishedDto = JsonSerializer.Deserialize<PlatfromPublishedDto>(platformPublishedMessage);

                try
                {
                    var platfrom = _mapper.Map<Platform>(platfromPublishedDto);
                    if (!repo.ExternalPlatfromExists(platfrom.ExternalID))
                    {
                        repo.CreatePlatfrom(platfrom);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Platfrom Already Exists in DB.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Can't Add Platfrom To DataBase :- {ex.Message}");
                }
            }
        }
    }


}

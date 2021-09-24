using AutoMapper;
using CommandService.Dtos;
using CommandService.Enums;
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
            throw new NotImplementedException();
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
    }


}

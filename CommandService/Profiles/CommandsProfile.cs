using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //map work as source to target 
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandReadDto, Command>();
            CreateMap<Command, CommandReadDto>();
        }
    }
}

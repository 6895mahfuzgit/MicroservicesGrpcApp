using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using PlatformService;

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
            CreateMap<PlatfromPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(x => x.ExternalID, obj => obj.MapFrom(src => src.PlatformId))
                .ForMember(x => x.Name, obj => obj.MapFrom(src => src.Name))
                .ForMember(x => x.Commands, obj => obj.Ignore())
                ;


        }
    }
}

using AutoMapper;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatfromCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatfromPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(x => x.PlatformId, obj => obj.MapFrom(src => src.Id))
                .ForMember(x => x.Name, obj => obj.MapFrom(src => src.Name))
                .ForMember(x => x.Publisher, obj => obj.MapFrom(src => src.Publisher))
                ;
        }
    }
}

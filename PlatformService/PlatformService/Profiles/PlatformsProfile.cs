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
        }
    }
}

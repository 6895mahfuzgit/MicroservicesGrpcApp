using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatfroms()
        {
            try
            {
                var platfromItems = _platformRepo.GetAllPlatfrom();
                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platfromItems));
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{id}")]
        public ActionResult<PlatformReadDto> GetPlatfromById(int id)
        {
            try
            {
                var platfrom = _platformRepo.GetPlatform(id);
                if (platfrom != null)
                {
                    return Ok(_mapper.Map<PlatformReadDto>(platfrom));
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

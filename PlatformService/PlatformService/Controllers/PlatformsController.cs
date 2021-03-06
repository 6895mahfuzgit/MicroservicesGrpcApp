using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices;
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
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
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


        [HttpGet("{id}", Name = "GetPlatfromById")]
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

        [HttpPost]
        public ActionResult<PlatformReadDto> CreaePlatfrom(PlatfromCreateDto platfromDto)
        {
            try
            {
                var platfromToSave = _mapper.Map<Platform>(platfromDto);
                _platformRepo.CreatePlatfrom(platfromToSave);
                _platformRepo.SaveChanges();

                var plafromResult = _mapper.Map<PlatformReadDto>(platfromToSave);

                //Syncr Message send
                try
                {
                    _commandDataClient.SendPlatformToCommand(plafromResult);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"MSG Failed To Send Sync Message From Controller {ex.Message}");
                }


                //Async Message Send
                try
                {
                    try
                    {
                        var platfromPublishedDto = _mapper.Map<PlatfromPublishedDto>(plafromResult);
                        platfromPublishedDto.Event = "Platform_Published";
                        _messageBusClient.PublishNewPlatform(platfromPublishedDto);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"MSG Failed To Send Async Message From Controller {ex.Message}");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                return CreatedAtRoute(nameof(GetPlatfromById), new { Id = plafromResult.Id }, plafromResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

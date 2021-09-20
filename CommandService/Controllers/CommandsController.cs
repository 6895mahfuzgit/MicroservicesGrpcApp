using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            try
            {
                Console.WriteLine(" Called GetCommandsForPlatform Action");
                if (!_commandRepo.PlatfromExists(platformId))
                {
                    return NotFound();
                }

                var commands = _commandRepo.GetCommandsForPlatform(platformId);
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            try
            {
                Console.WriteLine($"Called GetCommandForPlatform where PlatfromIs : {platformId}   commandId: {commandId} ");

                if (!_commandRepo.PlatfromExists(platformId))
                {
                    return NotFound();
                }

                var command = _commandRepo.GetCommand(platformId, commandId);
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

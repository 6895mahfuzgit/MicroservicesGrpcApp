﻿using AutoMapper;
using CommandService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {

        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult TestIndex()
        {
            try
            {
                Console.WriteLine("Index Action ");
                return Ok("Requested Successfully.");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

﻿using Microsoft.AspNetCore.Http;
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

        public PlatformsController()
        {

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

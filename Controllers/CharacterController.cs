using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet6_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();

        [HttpGet]
        public ActionResult<Character> Get(){
            return Ok(knight);
        }
    }
}
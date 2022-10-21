using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet6_api.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet6_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public ICharacterService CharacterService { get; }

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("")]
        public ActionResult<List<Character>> Get(){
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id){
            return Ok(_characterService.GetCharacterById(id));
        }

        [HttpPost("")]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter){
            return Ok(_characterService.AddCharacter(newCharacter));
        }
    }
}
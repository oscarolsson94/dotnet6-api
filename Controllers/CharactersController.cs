using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet6_api.Dtos.Character;
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

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id){
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id){
            var response = await _characterService.DeleteCharacter(id);

            if(response.Data == null) return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter){
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter){
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            if(response.Data == null) return NotFound(response);

            return Ok(response);
        }
    }
}
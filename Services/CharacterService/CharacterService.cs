using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet6_api.Dtos.Character;

namespace dotnet6_api.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharacter);

            character.Id = characters.Max(character => character.Id) + 1;

            characters.Add(character);

            serviceResponse.Data = characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

            try{
                Character character = characters.First(character => character.Id == id);
                
                characters.Remove(character);

                characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();
            }
            catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }


            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var character = characters.FirstOrDefault(character => character.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try{
                Character character = characters.FirstOrDefault(character => character.Id == updatedCharacter.Id);
                
                _mapper.Map<Character>(updatedCharacter);

                /* character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class; */

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }


            return response;
        }
    }
}
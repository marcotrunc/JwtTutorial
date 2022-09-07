using JwtTutorial.SuperHeroesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JwtTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int superHeroId ) 
        {
            var characters = await _context.Characters.Where(c => c.SuperHeroId == superHeroId)
                .Include(c=> c.Weapon)
                .ToListAsync();
            return Ok(characters);
        }
        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(CreateCharacterDto req)
        {
            SuperHero s = await _context.SuperHeroes.FindAsync(req.SuperHeroId);
            if (s == null)
                return BadRequest("Hero Not Found");
            Character newCharacter = new Character
            { 
                Name = req.Name,
                RpgClass = req.RpgClass,
                SuperHero = s,
                SuperHeroId = req.SuperHeroId
            };

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return  await Get(newCharacter.SuperHeroId);
        }
        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateCharacter(UpdateCharacterDto req)
        {
            Character character = await _context.Characters.Where(c => (c.Id == req.Id)).FirstOrDefaultAsync();
            if(character == null)
                return BadRequest("Il personaggio non è stato Trovato");
            character.SuperHeroId = req.SuperHeroId;
            character.Name = req.Name;
            character.RpgClass = req.RpgClass;
            await _context.SaveChangesAsync();
            return await Get(character.SuperHeroId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            Character character = await _context.Characters.FindAsync(id);
            if (character == null)
                return BadRequest("Character Not Found");
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return Ok("Personaggio Cancellato");
        }
    }
}

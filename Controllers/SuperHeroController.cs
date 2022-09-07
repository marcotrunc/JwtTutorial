using JwtTutorial.SuperHeroesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(await _context.SuperHeroes.ToListAsync());

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            SuperHero dbHero = await _context.SuperHeroes.FindAsync(id); 
            if (dbHero == null)
                return BadRequest("Hero not Found");
            return Ok(dbHero);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero([FromBody]SuperHero req)
        {
            SuperHero dbHero = await _context.SuperHeroes.FindAsync(req.Id);
            if (dbHero == null)
                return BadRequest("Hero not Found");
            dbHero.Name = req.Name;
            dbHero.Firstname = req.Firstname;
            dbHero.Lastname = req.Lastname;
            dbHero.Place = req.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            SuperHero dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not Found");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}

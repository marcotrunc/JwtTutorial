using JwtTutorial.SuperHeroesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero {
                Id = 1,
                Name = "SpiderMan",
                Firstname = "Peter",
                Lastname = "Parker",
                Place = "New York City"}
        };
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(heroes);

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            SuperHero hero = heroes.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not Found");
            return Ok(hero);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero([FromBody]SuperHero req)
        {
            SuperHero hero = heroes.Find(h => h.Id == req.Id);
            if (hero == null)
                return BadRequest("Hero not Found");
            hero.Name = req.Name;
            hero.Firstname = req.Firstname;
            hero.Lastname = req.Lastname;
            hero.Place = req.Place;
            return Ok(heroes);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            SuperHero hero = heroes.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not Found");
            heroes.Remove(hero);
            return Ok(heroes);
        }
    }
}

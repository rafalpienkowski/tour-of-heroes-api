using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TourOfHeroes.Api.Heroes
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly IHeroesRepository _heroesRepository;

        public HeroesController(ILogger<HeroesController> logger,IHeroesRepository heroesRepository)
        {
            _heroesRepository = heroesRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Hero> GetById(int id)
        {
            return await _heroesRepository.GetById(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Hero hero)
        {
            await _heroesRepository.Update(hero);
            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Hero hero)
        {
            var newHero = await _heroesRepository.Add(hero);
            return Created($"http://localhost:5000/api/heroes/{newHero.Id}", newHero);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _heroesRepository.Delete(id);
            return Accepted();
        }

        [HttpGet]
        public async Task<IList<Hero>> Search([FromQuery]string name)
        {
            return await _heroesRepository.SearchByName(name);
        }
    }
}
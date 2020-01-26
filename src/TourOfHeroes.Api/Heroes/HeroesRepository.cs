using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TourOfHeroes.Api.Heroes
{
    public class HeroesRepository : IHeroesRepository
    {
        private static IList<Hero> _heroes = new List<Hero>
        {
            new Hero { Id = 11, Name = "Dr Nice"},
            new Hero { Id = 12, Name = "Narco"},
            new Hero { Id = 13, Name = "Bombasto"},
            new Hero { Id = 14, Name = "Celeritas"},
            new Hero { Id = 15, Name = "Magneta"},
            new Hero { Id = 16, Name = "RubberMan"},
            new Hero { Id = 17, Name = "Dynama"},
            new Hero { Id = 18, Name = "Dr IQ"},
            new Hero { Id = 19, Name = "Magma"},
            new Hero { Id = 20, Name = "Tornado"},
        };

        public Task<Hero> Add(Hero hero)
        {   
            hero.Id = _heroes.OrderByDescending(h=> h.Id).Select(h => h.Id).FirstOrDefault() + 1;
            _heroes.Add(hero);

            return Task.FromResult(hero);
        }

        public async Task Delete(int id)
        {
            var heroToRemove = await GetById(id);
            if (heroToRemove == null)
            {
                //TODO 
            }
            _heroes.Remove(heroToRemove);
        }

        public Task<Hero> GetById(int id)
        {
            return Task.FromResult(_heroes.FirstOrDefault(h => h.Id == id));
        }

        public Task<IList<Hero>> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Task.FromResult((IList<Hero>)(_heroes.OrderBy(h => h.Id).ToList()));
            }
            return Task.FromResult((IList<Hero>)(_heroes.Where(h => h.Name.StartsWith(name, System.StringComparison.InvariantCultureIgnoreCase)).OrderBy(h => h.Id).ToList()));
        }

        public async Task Update(Hero hero)
        {
            var heroToUpdate = await GetById(hero.Id);
            if (heroToUpdate == null)
            {
                //TODO   
            }
            await Delete(heroToUpdate.Id);
            await Add(hero);
        }
    }
}
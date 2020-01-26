using System.Collections.Generic;
using System.Threading.Tasks;

namespace TourOfHeroes.Api.Heroes
{
    public interface IHeroesRepository
    {
        Task<Hero> GetById(int id);
        Task Update(Hero hero);
        Task<Hero> Add(Hero hero);
        Task Delete(int id);
        Task<IList<Hero>> SearchByName(string name);
    }
}
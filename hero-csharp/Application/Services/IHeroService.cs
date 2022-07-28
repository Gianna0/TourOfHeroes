using hero_csharp.Models;

namespace hero_csharp.Services;

public interface IHeroService
{
    Task<Hero> AddAsync(Hero hero);
    Task RemoveAsync(int id);
    Task<Hero> UpdateAsync(Hero hero);
    Task<Hero?> GetAsync(int id);
    Task<Hero?> GetAsync(string name);
    Task <IEnumerable<Hero>> GetAllAsync();
}

using hero_csharp.Models;
using hero_csharp.Repositories;

namespace hero_csharp.Services;

public class HeroService : IHeroService
{
    private readonly IHeroRepository _heroRepository;

    public HeroService(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    public async Task<Hero> AddAsync(Hero hero)
    {
        var newHero = Hero.Create(hero.Name);
        await _heroRepository.AddAsync(newHero);
        return newHero;
    }

    public async Task<Hero?> GetAsync(int id)
    {
        return await _heroRepository.GetAsync(id);
    }

    public async Task<Hero?> GetAsync(string name)
    {
        return await _heroRepository.GetAsync(name);
    }

    public Task<IEnumerable<Hero>> GetAllAsync()
    {
        return _heroRepository.GetAllAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var hero = await _heroRepository.GetAsync(id);

        if(hero is null)
        {
            throw new InvalidOperationException($"Can not find hero with id {id}");
        }

        await _heroRepository.RemoveAsync(hero);
    }

    public async Task<Hero> UpdateAsync(Hero hero)
    {
        var heroToUpdate = await _heroRepository.GetAsync(hero.Id);

        if(heroToUpdate is null)
        {
            throw new InvalidOperationException($"Can not find hero with id {hero.Id}");
        }
        heroToUpdate.ChangeName(hero.Name);
        await _heroRepository.UpdateAsync(heroToUpdate);
        return heroToUpdate;
    }
}
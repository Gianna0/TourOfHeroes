using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hero_csharp.Models;

namespace hero_csharp.Repositories;

public class InMemoryHeroRepository : IHeroRepository
{
    private static ISet<Hero> _heroes = new HashSet<Hero>();

    public async Task<Hero?> GetAsync(int id)
        => await Task.FromResult(_heroes.SingleOrDefault(x => x.Id == id));

    public async Task<Hero?> GetAsync(string name)
        => await Task.FromResult(_heroes.SingleOrDefault(x => x.Name.StartsWith(name)));

    public async Task<IEnumerable<Hero>> GetAllAsync()
        => await Task.FromResult(_heroes);

    public async Task AddAsync(Hero hero)
    {
        var id = await GetLastIdAsync();
        var propertyId = typeof(Hero).GetProperty("Id");
        propertyId?.SetValue(hero, id + 1);
        _heroes.Add(hero);
    }

    public async Task RemoveAsync(int id)
    {
        var hero = await GetAsync(id);
        _heroes.Remove(hero);
    }

    public Task RemoveAsync(Hero hero)
    {
        _heroes.Remove(hero);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Hero hero)
    {
        // jeszcze nic, potrz ebna baza danych
        return Task.CompletedTask;
    }

    private Task <int> GetLastIdAsync()
    {
        if (_heroes.Count == 0) 
        {
            return Task.FromResult(0);
        }

        return Task.FromResult(_heroes.Max(h => h.Id));
    }
}

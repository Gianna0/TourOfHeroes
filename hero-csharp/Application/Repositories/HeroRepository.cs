using Microsoft.EntityFrameworkCore;
using hero_csharp.Database;
using hero_csharp.Models;

namespace hero_csharp.Repositories;

public class HeroRepository : IHeroRepository
{
    private readonly Context _dbContext;

    public HeroRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Hero?> GetAsync(int id)
        => _dbContext.Heroes.Where(h => h.Id == id).SingleOrDefaultAsync();

    public Task<Hero?> GetAsync(string name)
        => _dbContext.Heroes.Where(h => h.Name.StartsWith(name)).SingleOrDefaultAsync();

    public Task<IEnumerable<Hero>> GetAllAsync()
        =>  Task.FromResult(_dbContext.Heroes.AsEnumerable());

    public async Task AddAsync(Hero hero)
    {
        await _dbContext.Heroes.AddAsync(hero);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var hero = await GetAsync(id);
        _dbContext.Heroes.Remove(hero);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Hero hero)
    {
        _dbContext.Heroes.Remove(hero);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hero hero)
    {
        _dbContext.Heroes.Update(hero);
        await _dbContext.SaveChangesAsync();
    }    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hero_csharp.Models;

namespace hero_csharp.Repositories;

public interface IHeroRepository
{
    Task<Hero?> GetAsync(int id);
    Task<Hero?> GetAsync(string name);
    Task<IEnumerable<Hero>> GetAllAsync(); //wszyscy uzytkownicy
    Task AddAsync(Hero hero);
    Task UpdateAsync(Hero hero);
    Task RemoveAsync(int id); 
    Task RemoveAsync(Hero hero);     
}

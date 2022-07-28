using Microsoft.AspNetCore.Mvc;
using hero_csharp.Services;
using hero_csharp.Models;

namespace hero_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeroesController : ControllerBase
{
    private readonly ILogger<HeroesController> _logger;
    private readonly IHeroService _heroService;

    public HeroesController(ILogger<HeroesController> logger, IHeroService heroService)
    {
        _logger = logger;
        _heroService = heroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hero>>> GetAllAsync()
    {
        _logger.LogInformation("Getting all heroes");
        return Ok(await _heroService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Hero>> GetAsync(int id)
    {
        _logger.LogInformation($"Getting hero with id: {id}");
        var hero = await _heroService.GetAsync(id);

        if (hero is null) 
        {
            return NotFound();
        }

        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<Hero>> AddAsync(Hero hero)
    {
        _logger.LogInformation("Adding hero");
        return Ok(await _heroService.AddAsync(hero));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Hero>> UpdateAsync(int id, Hero hero)
    {
        var newHero = new Hero(id, hero.Name);
        _logger.LogInformation("Updating Hero.");
        return Ok(await _heroService.UpdateAsync(newHero));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        _logger.LogInformation($"Deleting hero with id: {id}");
        await _heroService.RemoveAsync(id);
        return Ok();
    }
}

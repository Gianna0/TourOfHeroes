using hero_csharp.Models;
using hero_csharp.Repositories;
using hero_csharp.Services;
using Moq;
using Shouldly;

namespace Heroes.UnitTests.Services;

public class HeroServiceTests
{
    private HeroService heroService;
    private Mock<IHeroRepository> heroRepository;

    [SetUp]
    public void Setup()
    {
        heroRepository = new Mock<IHeroRepository>();
        heroService = new HeroService(heroRepository.Object);
    }

    [Test]
    public async Task should_add_hero()
    {
        var hero = Hero.Create("Tuptas");

        var heroAdded = await heroService.AddAsync(hero);

        heroAdded.ShouldNotBeNull();
        heroRepository.Verify(h => h.AddAsync(It.IsAny<Hero>()), times: Times.Once);
    }

    [Test]
    public async Task invalid_id_when_remove_should_throw_exception()
    {
        var id = 1;

        var exception = await Should.ThrowAsync<Exception>(() => heroService.RemoveAsync(id));
        
        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Can not find hero with id");
    }

    [Test]
    public async Task should_remove_hero()
    {
        var id = 1;
        var hero = new Hero(id, "Agent Pe");
        // setup mock tylko dla id=1 zwroci za kazdym razem obiekt hero
        heroRepository.Setup(h => h.GetAsync(id)).Returns(Task.FromResult(hero));

        await heroService.RemoveAsync(id);
        
        // weryfikuje czy metoda RemoveAsync na obiekcie zostala wywolana 1 raz
        heroRepository.Verify(h => h.RemoveAsync(hero), times: Times.Once);
    }

    [Test]
    public async Task invalid_id_when_update_should_throw_exception()
    {
        var hero = new Hero(4, "Gianna");

        var exception = await Should.ThrowAsync<Exception>(() => heroService.UpdateAsync(hero));

        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Can not find hero with id");
    }

    [Test]
    public async Task should_update_hero()
    {
        var id = 1;
        var hero = new Hero(id, "Agent Pe");
        var newHero = new Hero(id, "Pepe");
        // setup mock tylko dla id=1 zwroci za kazdym razem obiekt hero
        heroRepository.Setup(h => h.GetAsync(id)).Returns(Task.FromResult(hero));

        var heroUpdated = await heroService.UpdateAsync(newHero);

        heroUpdated.ShouldNotBeNull();
        heroUpdated.Name.ShouldBe(newHero.Name);
        heroRepository.Verify(h => h.UpdateAsync(It.IsAny<Hero>()), times: Times.Once);
    }
}

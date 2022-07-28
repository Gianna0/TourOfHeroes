using hero_csharp.Models;
using Shouldly;

namespace Heroes.UnitTests.Models;

public class HeroTests
{
    private Hero Act(string name) => Hero.Create(name);

    [Test]
    public void should_create_hero()
    {
        var name = "Tuptas";

        var hero = Act(name);

        hero.ShouldNotBeNull();
        hero.Name.ShouldBe(name);
    }

    [Test]
    public void null_name_should_throw_exception()
    {
        string name = null;

        var exception = Should.Throw<Exception>(() =>  Act(name));

        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Name can not be empty");
    }

    [Test]
    public void white_space_name_should_throw_exception()
    {
        string name = " ";

        var exception = Should.Throw<Exception>(() => Act(name));

        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Name can not be empty");
    }

    [Test]
    public void empty_name_should_throw_exception()
    {
        string name = "";

        var exception = Should.Throw<Exception>(() => Act(name));

        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Name can not be empty");
    }

    [Test]
    public void too_short_name()
    {
        string name = "AS";

        var exception = Should.Throw<Exception>(() => Act(name));

        exception.ShouldBeOfType<InvalidOperationException>();
        exception.Message.ShouldContain("Name should have at least 3 characters.");
    }

}

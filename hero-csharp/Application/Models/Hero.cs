using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hero_csharp.Models;

public class Hero
{
    public int Id { get; protected set;}
    public string Name { get; protected set;}

    public Hero(int id, string name)
    {
        Id = id;
        ChangeName(name);
    }

    public static Hero Create(string name)
    {
        return new Hero(0, name);
    }

    public void ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Name can not be empty.");
        }

        if(name.Length < 3)
        {
            throw new InvalidOperationException("Name should have at least 3 characters.");
        }

        Name = name;

    }
}

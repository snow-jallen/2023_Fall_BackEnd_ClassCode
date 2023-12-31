﻿using EfCoreDemo.Exceptions;

namespace Recapi.Data;

public class Recipe
{
    public Recipe(string name)
    {
        Name = name ?? throw new MissingNameException();
    }

    public int Id { get; set; }
    public string Name { get; private set; }
    public string Instructions { get; set; }
    public int MinutesToMake { get; set; }
    public string SourceUrl { get; set; }
    public string ImageUrl { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<CategorizedRecipe> Categories { get; set; } = new();
}

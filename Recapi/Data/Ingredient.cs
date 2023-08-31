﻿namespace Recapi.Data;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public List<RecipeIngredient>? RecipesUsingThisIngredient { get; set; }
}
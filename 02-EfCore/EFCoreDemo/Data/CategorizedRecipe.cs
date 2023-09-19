﻿namespace Recapi.Data;

public class CategorizedRecipe
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int CategoryId { get; set; }
    public Recipe Recipe { get; set; }
    public Category Category { get; set; }
}

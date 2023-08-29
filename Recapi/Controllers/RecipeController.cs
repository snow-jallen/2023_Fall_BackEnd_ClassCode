using Microsoft.AspNetCore.Mvc;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private static List<Recipe> _recipes = new();

    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<Recipe> Get()
    {
        return _recipes;
    }

    [HttpGet("{id}")]
    public Recipe Get(int id)
    {
        return _recipes[id];
    }

    [HttpPost]
    public Recipe Post([FromBody] Recipe recipe)
    {
        recipe.Id = _recipes.Count;
        _recipes.Add(recipe);
        return recipe;
    }
}

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}

public class Ingredient
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}
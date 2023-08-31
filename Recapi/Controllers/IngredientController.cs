using Microsoft.AspNetCore.Mvc;
using Recapi.Data;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IDataStore dataStore;

    public IngredientController(ILogger<RecipeController> logger, IDataStore dataStore)
    {
        _logger = logger;
        this.dataStore = dataStore;
    }

    [HttpGet()]
    public async Task<IEnumerable<Ingredient>> Get()
    {
        return await dataStore.GetAllIngredients();
    }

    [HttpGet("{id}")]
    public async Task<Ingredient> Get(int id)
    {
        return await dataStore.GetIngredient(id);
    }

    [HttpPost]
    public async Task<Ingredient> Post([FromBody] Ingredient ingredient)
    {
        var newRecipe = await dataStore.AddIngredient(ingredient);
        return newRecipe;
    }
}


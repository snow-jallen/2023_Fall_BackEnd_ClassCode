using Microsoft.AspNetCore.Mvc;
using Recapi.Data;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeIngredientController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IDataStore dataStore;

    public RecipeIngredientController(ILogger<RecipeController> logger, IDataStore dataStore)
    {
        _logger = logger;
        this.dataStore = dataStore;
    }

    [HttpPost]
    public async Task<RecipeIngredient> Post([FromBody] AddRecipeIngredientRequest request)
    {
        return await dataStore.AddRecipeIngredient(request);
    }
}

public class AddRecipeIngredientRequest
{
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
}

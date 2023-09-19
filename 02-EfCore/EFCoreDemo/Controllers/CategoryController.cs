using EfCoreDemo.DTO;
using Microsoft.AspNetCore.Mvc;
using Recapi.Data;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IDataStore dataStore;

    public CategoryController(ILogger<RecipeController> logger, IDataStore dataStore)
    {
        _logger = logger;
        this.dataStore = dataStore;
    }

    [HttpGet()]
    public async Task<IEnumerable<Category>> Get()
    {
        return await dataStore.GetAllCategories();
    }

    [HttpGet("{id}")]
    public async Task<Category> Get(int id)
    {
        return await dataStore.GetCategory(id);
    }

    [HttpGet("{id}/details")]
    public async Task<IResult> GetFullDetails(int id)
    {
        var category = await dataStore.GetCategory(id, showDetails: true);
        if (category == null)
        {
            return Results.NotFound("Invalid category id");
        }

        var recipes = category.Recipes.Select(r => new RecipeResult{ Id = r.RecipeId, Name = r.Recipe.Name, Instructions = r.Recipe.Instructions});
        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedOn = category.CreatedOn,
            Description = category.Description,
            Recipes = recipes
        };
        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<Category> Post([FromBody] Category category)
    {
        var newRecipe = await dataStore.AddCategory(category);
        return newRecipe;
    }
}


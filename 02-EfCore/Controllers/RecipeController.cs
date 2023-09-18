﻿using EfCoreDemo.DTO;
using Microsoft.AspNetCore.Mvc;
using Recapi.Data;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IDataStore dataStore;

    public RecipeController(ILogger<RecipeController> logger, IDataStore dataStore)
    {
        _logger = logger;
        this.dataStore = dataStore;
    }

    [HttpGet()]
    public async Task<IEnumerable<Recipe>> Get()
    {
        return await dataStore.GetAllRecipes();
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id, bool showDetails)
    {
        var recipe = await dataStore.GetRecipe(id, showDetails);
        if (recipe == null)
        {
            return Results.NotFound("Invalid recipe id");
        }

        if (showDetails)
        {
            var categories = recipe.Categories.Select(c => new CategoryDto { Id = c.CategoryId, Name = c.Category.Name, CreatedOn = c.Category.CreatedOn, Description = c.Category.Description });
            var ingredients = recipe.Ingredients.Select(i => new IngredientDto { Id = i.Id, Name = i.Name, Quantity = i.Quantity, Unit = i.Unit });
            var result = new RecipeResult
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Instructions = recipe.Instructions,
                Categories = categories,
                Ingredients = ingredients
            };
            return Results.Ok(result);
        }

        return Results.Ok(new RecipeResult{ Id = recipe.Id, Name = recipe.Name, Instructions = recipe.Instructions });
    }

    [HttpPost]
    public async Task<Recipe> Post([FromBody] Recipe recipe)
    {
        var newRecipe = await dataStore.AddRecipe(recipe);
        return newRecipe;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await dataStore.DeleteRecipe(id);
    }

    [HttpPost("categorize")]
    public async Task<IResult> CategorizeRecipe(CategorizeRecipeRequest request)
    {
        var recipe = await dataStore.GetRecipe(request.RecipeId);
        var category = await dataStore.GetCategory(request.CategoryId);
        if(recipe  == null || category == null)
        {
            return Results.BadRequest("Invalid recipe or category id");
        }
        await dataStore.CategorizeRecipe(recipe, category);
        return Results.Ok();
    }
}

public class CategorizeRecipeRequest
{
    public int RecipeId { get; set; }
    public int CategoryId { get; set; }
}


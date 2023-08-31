using Recapi.Controllers;
using System.Text.Json;

namespace Recapi.Data;

public class JsonDataStore : IDataStore
{
    List<Recipe> _recipes = new();
    List<Ingredient> _ingredients = new();
    List<RecipeIngredient> _recipeIngredients = new();
    private string recipePath = "recipes.json";
    private string ingredientPath = "ingredients.json";
    private string recipeIngredientPath = "recipeIngredients.json";

    public JsonDataStore()
    {
        readFromDisk();
    }

    private async Task saveToDiskAsync()
    {
        //save the lists to individual json files
        await System.IO.File.WriteAllTextAsync(recipePath, JsonSerializer.Serialize(_recipes));
        await System.IO.File.WriteAllTextAsync(ingredientPath, JsonSerializer.Serialize(_ingredients));
        await System.IO.File.WriteAllTextAsync(recipeIngredientPath, JsonSerializer.Serialize(_recipeIngredients));
    }

    private async Task readFromDisk()
    {
        if (System.IO.File.Exists(recipePath))
        {
            var recipeText = await File.ReadAllTextAsync(recipePath);
            _recipes = JsonSerializer.Deserialize<List<Recipe>>(recipeText);
        }
        if (System.IO.File.Exists(ingredientPath))
        {
            var ingredientText = await File.ReadAllTextAsync(ingredientPath);
            _ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientText);
        }
        if (System.IO.File.Exists(recipeIngredientPath))
        {
            var recipeIngredientText = await File.ReadAllTextAsync(recipeIngredientPath);
            _recipeIngredients = JsonSerializer.Deserialize<List<RecipeIngredient>>(recipeIngredientText);
        }
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        if (_recipes.Count == 0)
            recipe.Id = 1;
        else
            recipe.Id = _recipes.Max(r => r.Id) + 1;
        _recipes.Add(recipe);
        await saveToDiskAsync();
        return recipe;
    }

    public async Task DeleteRecipe(int id)
    {
        var numDeleted = _recipes.RemoveAll(r => r.Id == id);
        if (numDeleted == 0)
            throw new RecipeNotFoundException();
        await saveToDiskAsync();
    }

    public Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        return Task.FromResult<IEnumerable<Recipe>>(_recipes);
    }

    public async Task<Recipe> GetRecipe(int id)
    {
        await Task.Delay(1);
        return _recipes.FirstOrDefault(r => r.Id == id) ?? throw new RecipeNotFoundException();
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe)
    {
        var existingRecipe = _recipes.FirstOrDefault(r => r.Id == recipe.Id);
        if (existingRecipe is null)
            throw new RecipeNotFoundException();

        existingRecipe.Ingredients = recipe.Ingredients;
        existingRecipe.Instructions = recipe.Instructions;
        existingRecipe.Name = recipe.Name;

        await saveToDiskAsync();
        return existingRecipe;
    }

    public async Task<IEnumerable<Ingredient>> GetAllIngredients()
    {
        await Task.CompletedTask;
        return _ingredients;
    }

    public async Task<Ingredient> GetIngredient(int id)
    {
        await Task.CompletedTask;
        return _ingredients.FirstOrDefault(i => i.Id == id) ?? throw new IngredientNotFoundException();
    }

    public async Task<Ingredient> AddIngredient(Ingredient ingredient)
    {
        if (_ingredients.Count == 0)
            ingredient.Id = 1;
        else
            ingredient.Id = _ingredients.Max(i => i.Id) + 1;
        _ingredients.Add(ingredient);
        await saveToDiskAsync();
        return ingredient;
    }

    public async Task<RecipeIngredient> AddRecipeIngredient(AddRecipeIngredientRequest recipeIngredientRequest)
    {
        var recipeIngredient = new RecipeIngredient();
        if (_recipeIngredients.Count == 0)
            recipeIngredient.Id = 1;
        else
            recipeIngredient.Id = _recipeIngredients.Max(ri => ri.Id) + 1;

        recipeIngredient.Recipe = await GetRecipe(recipeIngredientRequest.RecipeId);
        recipeIngredient.Ingredient = await GetIngredient(recipeIngredientRequest.IngredientId);

        _recipeIngredients.Add(recipeIngredient);
        await saveToDiskAsync();
        return recipeIngredient;
    }
}

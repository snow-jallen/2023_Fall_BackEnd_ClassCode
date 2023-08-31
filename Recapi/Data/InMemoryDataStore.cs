using Recapi.Controllers;

namespace Recapi.Data;

public class InMemoryDataStore : IDataStore
{
    private List<Recipe> recipes = new List<Recipe>();

    public Task<Ingredient> AddIngredient(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> AddRecipe(Recipe recipe)
    {
        if (recipe.Id == 0)
        {
            recipe.Id = recipes.Count + 1;
        }
        recipes.Add(recipe);
        return Task.FromResult(recipe);
    }

    public Task<RecipeIngredient> AddRecipeIngredient(AddRecipeIngredientRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRecipe(int id)
    {
        await Task.CompletedTask;
        recipes.RemoveAll(r => r.Id == id);
    }

    public Task<IEnumerable<Ingredient>> GetAllIngredients()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        await Task.CompletedTask;
        return recipes;
    }

    public Task<Ingredient> GetIngredient(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> GetRecipe(int id)
    {
        return Task.FromResult(recipes.FirstOrDefault(r => r.Id == id));
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe)
    {
        await DeleteRecipe(recipe.Id);
        return await AddRecipe(recipe);
    }
}

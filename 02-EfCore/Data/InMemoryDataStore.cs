namespace Recapi.Data;

public class InMemoryDataStore : IDataStore
{
    private List<Recipe> recipes = new List<Recipe>();
    public Task<Recipe> AddRecipe(Recipe recipe)
    {
        if (recipe.Id == 0)
        {
            recipe.Id = recipes.Count + 1;
        }
        recipes.Add(recipe);
        return Task.FromResult(recipe);
    }

    public Task DeleteRecipe(int id)
    {
        recipes.RemoveAll(r => r.Id == id);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        await Task.CompletedTask;
        return recipes;
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

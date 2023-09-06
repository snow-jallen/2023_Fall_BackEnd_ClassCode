using Microsoft.EntityFrameworkCore;

namespace Recapi.Data;

public class PostgresDataStore : IDataStore
{
    private readonly RecipeContext context;

    public PostgresDataStore(RecipeContext context)
    {
        this.context = context;
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }

    public Task DeleteRecipe(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        return await context.Recipes.ToListAsync();
    }

    public Task<Recipe> GetRecipe(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> UpdateRecipe(Recipe recipe)
    {
        throw new NotImplementedException();
    }
}

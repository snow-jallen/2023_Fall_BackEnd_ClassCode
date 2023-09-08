using Microsoft.EntityFrameworkCore;

namespace Recapi.Data;

public class PostgresDataStore : IDataStore
{
    private readonly RecipeContext context;
    private readonly ILogger<PostgresDataStore> logger;

    public PostgresDataStore(RecipeContext context, ILogger<PostgresDataStore> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }

    public async Task DeleteRecipe(int id)
    {
        var existingRecipe = await context.Recipes.FindAsync(id);
        if (existingRecipe is null)
        {
            throw new ArgumentException($"Recipe with id {id} does not exist");
        }
        context.Recipes.Remove(existingRecipe);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        logger.LogInformation("Trying to get recipes...");
        var recipes = await context.Recipes
            .Include(r => r.Ingredients)
            .ToListAsync();

        return recipes;
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

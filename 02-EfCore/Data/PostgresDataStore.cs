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

    public async Task<Category> AddCategory(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }

    public async Task CategorizeRecipe(Recipe recipe, Category category)
    {
        context.CategorizedRecipes.Add(new CategorizedRecipe { RecipeId = recipe.Id, CategoryId = category.Id });
        await context.SaveChangesAsync();
    }

    public Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
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

    public async Task<IEnumerable<Category>> GetAllCategories() => await context.Categories.ToListAsync();

    public async Task<IEnumerable<Recipe>> GetAllRecipes() => await context.Recipes.ToListAsync();

    public async Task<Category> GetCategory(int id, bool showDetails = false)
    {
        if (showDetails)
        {
            return await context.Categories
                .Include(c => c.Recipes)
                .ThenInclude(c => c.Recipe)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        return await context.Categories.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recipe> GetRecipe(int id, bool showDetails = false)
    {
        if (showDetails)
            return await context.Recipes
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
        return await context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<Category> UpdateCategory(Category Category)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> UpdateRecipe(Recipe recipe)
    {
        throw new NotImplementedException();
    }
}

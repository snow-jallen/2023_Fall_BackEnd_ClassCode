namespace Recapi.Data;

public interface IDataStore
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipe(int id);
    Task<Recipe> AddRecipe(Recipe recipe);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task DeleteRecipe(int id);
}

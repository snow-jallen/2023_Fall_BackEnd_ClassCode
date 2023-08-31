using Recapi.Controllers;

namespace Recapi.Data;

public interface IDataStore
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipe(int id);
    Task<Recipe> AddRecipe(Recipe recipe);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task DeleteRecipe(int id);
    Task<IEnumerable<Ingredient>> GetAllIngredients();
    Task<Ingredient> GetIngredient(int id);
    Task<Ingredient> AddIngredient(Ingredient ingredient);
    Task<RecipeIngredient> AddRecipeIngredient(AddRecipeIngredientRequest addRecipeIngredientRequest);
}

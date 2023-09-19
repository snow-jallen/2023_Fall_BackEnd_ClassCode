namespace Recapi.Data;

public interface IDataStore
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipe(int id, bool showDetails=false);
    Task<Recipe> AddRecipe(Recipe recipe);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task DeleteRecipe(int id);

    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategory(int id, bool showDetails=false);
    Task<Category> AddCategory(Category Category);
    Task<Category> UpdateCategory(Category Category);
    Task DeleteCategory(int id);

    Task CategorizeRecipe(Recipe recipe, Category category);
}

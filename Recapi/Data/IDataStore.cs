namespace Recapi.Data;

public interface IDataStore
{
    IEnumerable<Recipe> GetAllRecipes();
    Recipe GetRecipe(int id);
    Recipe AddRecipe(Recipe recipe);
    Recipe UpdateRecipe(Recipe recipe);
    void DeleteRecipe(int id);
}

public class InMemoryDataStore : IDataStore
{
    private List<Recipe> recipes = new List<Recipe>();
    public Recipe AddRecipe(Recipe recipe)
    {
        if (recipe.Id == 0)
        {
            recipe.Id = recipes.Count + 1;
        }
        recipes.Add(recipe);
        return recipe;
    }

    public void DeleteRecipe(int id)
    {
        recipes.RemoveAll(r => r.Id == id);
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        return recipes;
    }

    public Recipe GetRecipe(int id)
    {
        return recipes.FirstOrDefault(r => r.Id == id);
    }

    public Recipe UpdateRecipe(Recipe recipe)
    {
        DeleteRecipe(recipe.Id);
        return AddRecipe(recipe);
    }
}

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}

public class Ingredient
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}
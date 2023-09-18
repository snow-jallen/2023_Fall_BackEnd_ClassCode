namespace Recapi.Data;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<CategorizedRecipe> Categories { get; set; } = new();
}

namespace Recapi.Data;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Instructions { get; set; }
    public List<RecipeIngredient>? Ingredients { get; set; }
}

namespace Recapi.Data;

public class RecipeIngredient
{
    public int Id { get; set; }
    public Recipe Recipe { get; set; }
    public Ingredient Ingredient { get; set; }
}

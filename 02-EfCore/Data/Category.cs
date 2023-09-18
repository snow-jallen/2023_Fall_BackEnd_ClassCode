namespace Recapi.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public List<CategorizedRecipe> Recipes { get; set; } = new();
}

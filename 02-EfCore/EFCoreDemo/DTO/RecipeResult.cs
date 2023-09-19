namespace EfCoreDemo.DTO;

public class RecipeResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public IEnumerable<IngredientDto> Ingredients { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public IEnumerable<RecipeResult> Recipes { get; set; }
}
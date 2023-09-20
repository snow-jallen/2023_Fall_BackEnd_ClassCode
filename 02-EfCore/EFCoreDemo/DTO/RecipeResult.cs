namespace EfCoreDemo.DTO;

public class RecipeResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public IEnumerable<IngredientDto> Ingredients { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}

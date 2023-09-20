namespace EfCoreDemo.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public IEnumerable<RecipeResult> Recipes { get; set; }
}
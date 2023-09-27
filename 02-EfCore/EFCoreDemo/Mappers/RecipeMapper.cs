using EfCoreDemo.DTO;
using Recapi.Data;
using Riok.Mapperly.Abstractions;

namespace EfCoreDemo.Mappers;

[Mapper]
public partial class RecipeMapper
{
    public partial CategoryDto CategoryToCategoryDto(Category category);
    public partial Category CategoryDtoToCategory(CategoryDto dto);
    public partial Recipe RecipeDtoToRecipe(RecipeDto dto);
    public partial RecipeDto RecipeToRecipeDto(Recipe recipe);
}

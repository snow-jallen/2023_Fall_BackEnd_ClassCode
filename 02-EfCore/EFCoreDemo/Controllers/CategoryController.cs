using EfCoreDemo.DTO;
using EfCoreDemo.Mappers;
using Microsoft.AspNetCore.Mvc;
using Recapi.Data;

namespace Recapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IDataStore dataStore;
    private readonly RecipeMapper mapper;

    public CategoryController(ILogger<RecipeController> logger, IDataStore dataStore, RecipeMapper mapper)
    {
        _logger = logger;
        this.dataStore = dataStore;
        this.mapper = mapper;
    }

    [HttpGet()]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        return (await dataStore.GetAllCategories()).Select(mapper.CategoryToCategoryDto);
    }

    [HttpGet("{id}")]
    public async Task<CategoryDto> Get(int id)
    {
        return mapper.CategoryToCategoryDto(await dataStore.GetCategory(id));
    }

    [HttpGet("{id}/details")]
    public async Task<IResult> GetFullDetails(int id)
    {
        var category = await dataStore.GetCategory(id, showDetails: true);
        if (category == null)
        {
            return Results.NotFound("Invalid category id");
        }

        var recipes = category.Recipes.Select(r => new RecipeResult{ Id = r.RecipeId, Name = r.Recipe.Name, Instructions = r.Recipe.Instructions});
        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedOn = category.CreatedOn,
            Description = category.Description,
            Recipes = recipes
        };
        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<CategoryDto> Post([FromBody] CategoryDto categoryDto)
    {
        var category = mapper.CategoryDtoToCategory(categoryDto);
        var newCategory = await dataStore.AddCategory(category);
        return mapper.CategoryToCategoryDto(newCategory);
    }
}


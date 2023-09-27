using EfCoreDemo.DTO;
using FluentAssertions;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ApiTests;

public class IntegrationTests : BaseIntegrationTest
{

    public IntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {        
    }

    [Fact]
    public async Task IntegrationTest1()
    {
        await Client.GetAsync("/recipe");

        var initialRecipeList = await Client.GetFromJsonAsync<IEnumerable<RecipeDto>>("/recipe");
        initialRecipeList.Should().BeEmpty();

        var newRecipe = new RecipeDto { Name = "Cookies", Instructions = "Bake them", ImageUrl = "img", SourceUrl = "src", MinutesToMake = 20 };
        var postResponse = await Client.PostAsJsonAsync("/recipe", newRecipe);
        var postedRecipe = await postResponse.Content.ReadFromJsonAsync<RecipeDto>();
        postedRecipe.Id.Should().BeGreaterThan(0);

        var updatedRecipeList = await Client.GetFromJsonAsync<IEnumerable<RecipeDto>>("/recipe");
        updatedRecipeList.Should().NotBeEmpty();
    }
}

public class Integration2 : BaseIntegrationTest
{
    public Integration2(IntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task NewTaskNewDatabase()
    {
        await Client.GetAsync("/recipe");
        try
        {
            var initialRecipeList = await Client.GetFromJsonAsync<IEnumerable<RecipeDto>>("/recipe");
            initialRecipeList.Should().BeEmpty();
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    public HttpClient Client { get; }

    private IServiceScope scope;

    public RecipeContext DbContext { get; private set; }

    public BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Client = factory.CreateDefaultClient();
        scope = factory.Services.CreateScope();        
        DbContext = scope.ServiceProvider.GetRequiredService<RecipeContext>();
    }

    public void Dispose()
    {
        scope?.Dispose();
        DbContext?.Dispose();
    }
}

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer dbContainer = new PostgreSqlBuilder()
        .WithPassword("password")        
        .Build();
    private readonly ITestOutputHelper testOutputHelper;

    //public IntegrationTestWebAppFactory(ITestOutputHelper testOutputHelper)
    //{
    //    this.testOutputHelper = testOutputHelper;
    //}

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType = typeof(DbContextOptions<RecipeContext>);
            var descriptor = services.SingleOrDefault(s=>s.ServiceType == descriptorType);
            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RecipeContext>(options => options.UseNpgsql(dbContainer.GetConnectionString()));

            builder.ConfigureLogging(p => p.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(testOutputHelper, appendScope: false)));
        });
    }

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await dbContainer.StopAsync();
    }
}

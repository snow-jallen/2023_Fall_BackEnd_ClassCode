using EfCoreDemo;
using Microsoft.EntityFrameworkCore;
using Recapi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataStore, PostgresDataStore>();//scoped not singleton
builder.Services.AddDbContext<RecipeContext>(options =>
{
    options.UseNpgsql(builder.Configuration["DbConnectionString"]);
});
builder.Services.AddHostedService<MigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

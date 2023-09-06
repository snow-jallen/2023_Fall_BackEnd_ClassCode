# 02 - Ef Core Demo

## Command to spin up a new postgres container using Docker

`docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=password postgres`

## Steps to add Postgres database support to your API
- Add the Npgsql.EntityFrameworkCore.PostgreSQL and Microsoft.EntityFrameworkCore.Design nuget packages to your project
- Create a PostgresContext class 
	- it inherits from DbContext
	- include the constructor that passes configuration to the base constructor
	- include any public `DbSet<T>` properties for any classes that you want to turn into tables
- Add the "DbConnectionString" to your appsettings.json file
- Update Program.cs to make your new DbContext class available via Dependency Injection (the `builder.Services.AddDbContext...` line)
- Create a migration
  - Install the dotnet ef tools `dotnet tool install -g dotnet-ef` (this just needs to happen once per machine)
  - `dotnet ef migrations add FirstMigration`
- code to apply migrations at startup (we'll go over this on Friday)
- new IDataService implementation that uses your new DbContext class
- register your new IDataService implementation in Program.cs (and make sure you register it with a Scoped lifetime, not a Singleton lifetime)

## EF Core commands

- `dotnet tool install -g dotnet-ef` This installs the command line tools necessary to work with ef core
- `dotnet ef migrations add MigrationNameSpelledLikeAClassName` Create a new database migration based off changes to classes referenced in your DbContext
- `dotnet ef database update` apply any new migrations to the database
- 

## Friday's class

- Migrations
  - Change the schema (add some columns), create the migration, run the app...it blows up.
  - Apply the migration manually
  - App works.
  - How can we do that automatically?
- Why are the recipe ingredients null?
- Service lifetimes
- Review 

```csharp
public class MigrationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<WhateverMyContextIs>();

            await db.Database.MigrateAsync(stoppingToken);
        }
    }
}
```
# 02 - Ef Core Demo

## Command to spin up a new postgres container using Docker

`docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=password postgres`

> If that worked properly, you should be able to run this command to run `psql` (the postgres command line interface) and connect to your database

`docker run -it --rm postgres psql -h host.docker.internal -U postgres`

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

> If you want to see that your migration was successfully applied, run `docker run -it --rm postgres psql -h host.docker.internal -U postgres` then 
> run `\c yourdatabasename` to connect to the database specified in your connection string.  Then run `\d` to list any tables.
> 
```bash
$> docker run -it --rm postgres psql -h host.docker.internal -U postgres
Password for user postgres:
psql (15.4 (Debian 15.4-1.pgdg120+1))
Type "help" for help.

postgres=# \c recipes
You are now connected to database "recipes" as user "postgres".
recipes=# \d
                  List of relations
 Schema |         Name          |   Type   |  Owner
--------+-----------------------+----------+----------
 public | Ingredients           | table    | postgres
 public | Ingredients_Id_seq    | sequence | postgres
 public | Recipes               | table    | postgres
 public | Recipes_Id_seq        | sequence | postgres
 public | __EFMigrationsHistory | table    | postgres
(5 rows)

recipes=#
```

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

## Live site running in azure:

[Azure API](https://appsvclinux2.azurewebsites.net/recipe)
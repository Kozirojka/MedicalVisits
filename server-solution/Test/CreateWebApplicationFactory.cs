using System.Data.Common;
using MedicalVisits.API;
using MedicalVisits.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using Xunit;

namespace Test;

public class CreateWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;

    public HttpClient HttpClient { get; private set; } = null!;

    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("MedicalVisits")
        .WithUsername("postgres")
        .WithPassword("admin")
        .Build();


    //контроль життя застосунку
    public async Task InitializeAsync()
    {
        await _postgresSqlContainer.StartAsync();

        _dbConnection = new NpgsqlConnection("Host=localhost;Port=5432;Database=MedicalVisits;Username=postgres;Password=admin");
    

        HttpClient = CreateClient();

        await _dbConnection.OpenAsync();
        await InitializeRespawnerAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgresSqlContainer.DisposeAsync();
        await _dbConnection.DisposeAsync();
    }

    /**
     * 9:45 маршутка
     * пів 12 Львів,
     * 12:20 дома
     *
     * У мене є 2 години вільного сидіння у хаті 
     *
     * Вийти з хати 14:40
     * Оперний 15:30
     * Фільм 16:00
     */
    
    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:Postgres",
            "Host=localhost;Port=5432;Database=MedicalVisits;Username=postgres;Password=admin");
    }

    private async Task InitializeRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            SchemasToInclude = ["medicalApp"],
            DbAdapter = DbAdapter.Postgres
        });
    }
}
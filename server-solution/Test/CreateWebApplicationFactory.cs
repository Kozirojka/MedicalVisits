using System.Data.Common;
using MedicalVisits.API;
using MedicalVisits.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
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

        HttpClient = CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _postgresSqlContainer.DisposeAsync();
    }
    
    
    // IWebHostBuilder дозволяє переконфігурувати сервіси у моєму застосунку
    // protected override void ConfigureWebHost(IWebHostBuilder builder)
    // {
    //
    //     string _connectionString = "Host=localhost;Port=5432;Database=MedicalVisits;Username=postgres;Password=admin";
    //     
    //     Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", _postgresSqlContainer.GetConnectionString());
    //     
    //     
    //     
    //     builder.ConfigureServices(services =>
    //     {
    //         services .Remove(services .SingleOrDefault(service => typeof(DbContextOptions<ApplicationDbContext>) == service.ServiceType));
    //         
    //         services .Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
    //         
    //         
    //         services.AddDbContext<ApplicationDbContext>((_, option) => option.UseNpgsql(_connectionString));
    //     });
    // }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:Postgres", _postgresSqlContainer.GetConnectionString());
    }
}
using MedicalVisits.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Testcontainers.PostgreSql;
using Xunit;

namespace Test;

public class CreateWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{

    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("MedicalVisits")
        .WithUsername("postgres")
        .WithPassword("admin")
        .Build();
    
    public HttpClient HttpClient { get; private set; } = null!;


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
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
    }
}
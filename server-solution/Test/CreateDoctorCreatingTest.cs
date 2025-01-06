using Xunit;

namespace Test;


[Collection("CreatingUser")]
public class CreateDoctorCreatingTest(CreateWebApplicationFactory factory) :  IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;
    
    
    
    
    public async Task DisposeAsync() => await factory.ResetDatabaseAsync();
}  
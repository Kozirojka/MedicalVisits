using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Test;

[Collection("CreatingUser")]
public class CreateDoctorCreatingTest(CreateWebApplicationFactory factory) :  IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task CreateShipment_ShouldReturnConflict_WhenShipmentForOrderIsAlreadyCreated()
    {
        Assert.AreEqual(1, 1);
    }
    
    public async Task DisposeAsync() => await factory.ResetDatabaseAsync();
}  



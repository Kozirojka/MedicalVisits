using MedicalVisits.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Test;

public class CreateWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
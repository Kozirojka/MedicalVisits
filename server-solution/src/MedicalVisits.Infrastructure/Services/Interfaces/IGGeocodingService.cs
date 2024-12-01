using MedicalVisits.Models;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IGGeocodingService
{
    public Task<(double Latitude, double Longitude)> GeocodeAddressAsync(Address address);
}

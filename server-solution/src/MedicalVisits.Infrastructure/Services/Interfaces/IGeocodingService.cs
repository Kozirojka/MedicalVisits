using MedicalVisits.Models;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IGeocodingService
{
    public Task<(double Latitude, double Longitude)> GeocodeAddressAsync(Address address);
}

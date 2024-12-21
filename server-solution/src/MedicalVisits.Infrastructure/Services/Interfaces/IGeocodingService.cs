using MedicalVisits.Models;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IGeocodingService
{
    public Task<Coordinate> GeocodeAddressAsync(Address address);
}

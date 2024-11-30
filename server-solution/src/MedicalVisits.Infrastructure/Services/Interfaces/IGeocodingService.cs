using MedicalVisits.Models;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IGeocodingService
{
     Task<List<double>?> GetCoordinatesFromAddress(Address _address);
     Task<double?> GetDistanceBetweenCoordinates(List<double> startCoordinates, List<double> endCoordinates);
     
}

using MedicalVisits.Models.diraction;
using MedicalVisits.Models.diraction.models;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IRouteService
{
    public Task<RouteResponse?> GetOptimizedRouteAsync(Coordinate start, List<Coordinate> waypoints);
    public Task<double> GetDistanceBetweenTwoPoints(Coordinate startPoint, Coordinate endPoint);

    public Task<List<DoctorProfileWithDistance>> CalculateDistancesAsync(List<DoctorProfile> doctors,
        Coordinate patientCoordinates);
}
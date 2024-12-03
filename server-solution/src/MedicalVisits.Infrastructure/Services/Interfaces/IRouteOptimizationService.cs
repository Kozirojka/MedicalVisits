using MedicalVisits.Models.diraction;
using MedicalVisits.Models.diraction.models;

namespace MedicalVisits.Infrastructure.Services.Interfaces;

public interface IRouteOptimizationService
{
    public Task<RouteResponse?> GetOptimizedRouteAsync(Coordinate start, List<Coordinate> waypoints);
    public Task<double> GetDistanceBetweenTwoPoints(Coordinate startPoint, Coordinate endPoint);

}
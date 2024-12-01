namespace MedicalVisits.Models.diraction.models;

public class RouteOptimizationRequest
{
    public Coordinate StartPoint { get; set; }
    public List<Coordinate> Waypoints { get; set; }
}
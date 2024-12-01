using MedicalVisits.Models.diraction.models;

namespace MedicalVisits.Models.diraction;

public class OptimizedRoute
{
    public List<Coordinate> OrderedWaypoints { get; set; }
    public double TotalDistance { get; set; }
    public int TotalDuration { get; set; }
    public List<string> Steps { get; set; }
    public string EncodedPolyline { get; set; }

    public OptimizedRoute()
    {
        OrderedWaypoints = new List<Coordinate>();
        Steps = new List<string>();
    }
}
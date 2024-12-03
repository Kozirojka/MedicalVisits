using MedicalVisits.Models.diraction.models;

namespace MedicalVisits.Models.diraction;

public class OptimizedRoute
{
    public double TotalDistance { get; set; }
    public int TotalDuration { get; set; }
    public string EncodedPolyline { get; set; }

    public OptimizedRoute(int totalDuration, string encodedPolyline)
    {
        TotalDuration = totalDuration;
        EncodedPolyline = encodedPolyline;
    }
}
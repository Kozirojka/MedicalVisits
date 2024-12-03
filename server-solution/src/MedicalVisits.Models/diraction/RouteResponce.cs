namespace MedicalVisits.Models.diraction;

public class RouteResponse
{
    public List<Route> Routes { get; set; }
}

public class Route
{
    public int DistanceMeters { get; set; }
    public string Duration { get; set; }
    public Polyline Polyline { get; set; }
}

public class Polyline
{
    public string EncodedPolyline { get; set; }
}
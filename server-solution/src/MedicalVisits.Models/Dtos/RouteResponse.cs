namespace MedicalVisits.Models.Dtos;

public class RouteResponse
{
    public List<Route> Routes { get; set; }
}

public class Route
{
    public double Duration { get; set; }
    public double Distance { get; set; }
    public string Geometry { get; set; } // Encoded polyline
    public List<Segment> Segments { get; set; }
}

public class Segment 
{
    public List<Step> Steps { get; set; }
}

public class Step
{
    public List<double> Location { get; set; }
    public double Duration { get; set; }
    public double Distance { get; set; }
}
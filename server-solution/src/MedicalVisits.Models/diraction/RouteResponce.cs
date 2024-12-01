namespace MedicalVisits.Models.diraction;

public class RouteResponse
{
    public List<Route> Routes { get; set; }
}

public class Route
{
    public int DistanceMeters { get; set; }
    public int Duration { get; set; }
    public List<int> OptimizedIntermediateWaypointIndex { get; set; }
    public List<RouteLeg> Legs { get; set; }
}

public class RouteLeg
{
    public List<RouteStep> Steps { get; set; }
}

public class RouteStep
{
    public string NavigationInstruction { get; set; }
}
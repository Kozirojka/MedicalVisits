using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommand : IRequest<RouteResponse>
{
    public GetConfirmVisitRequestsCommand(RouteRequestDto routeRequest)
    {
        RouteRequest = routeRequest;
    }

    public RouteRequestDto RouteRequest { get; set; }
}

//TODO: придумати як передвавтаи id не передаваючи його на пряму, можливо якось через claims
public class RouteRequestDto
{
    //Це як статус для пошуку
    public VisitStatus status = VisitStatus.DoctorAccepted;
    
    public string? DoctorId { get; set; }
    //Це буде як ті дні які хочеш найти доктор
    public DateTime DateStart = DateTime.Today;
    public DateTime DateEnd = DateTime.Today;
    public RouteRequestDto(string doctorId, DateTime dateEnd, DateTime dateStart)
    {
        DateEnd = dateEnd;
        DateStart = dateStart;
    }
}
public class RouteResponseDto
{
    public List<Coordinate> OptimizedRoute { get; set; }
    public double TotalDistance { get; set; }
    public string EncodedPath { get; set; }
}

public class Coordinate
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}
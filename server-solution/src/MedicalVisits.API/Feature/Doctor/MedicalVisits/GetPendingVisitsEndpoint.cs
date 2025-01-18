using System.Security.Claims;
using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits;

public class GetPendingVisitsEndpoint(IMediator _mediator) : EndpointWithoutRequest<
    Results<Ok<List<VisitRequestDtoNew>>,
    NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/v2/doctor/visits/pending-visits");
        Roles("Doctor");
    }
    
    
    public override async Task<Results<Ok<List<VisitRequestDtoNew>>, NotFound, ProblemDetails>> 
        ExecuteAsync(CancellationToken cancellationToken)
    {
        
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var dto = new DoctorRequestFilterDto 
        { 
            Id = doctorId,
            Status = VisitStatus.Approved
        };

        var getListOfAllVisitsToDoctor = new GetPendingRequestsForDoctorCommand(dto);
        var result = await _mediator.Send(getListOfAllVisitsToDoctor);
        

        return TypedResults.Ok(result);
    }
}
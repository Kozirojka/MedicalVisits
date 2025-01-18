using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits.GetPendingVisits;

public class GetPendingVisitsEndpoint(IMediator mediator, IUserService userService) : EndpointWithoutRequest<
    Results<Ok<List<VisitRequestResponce>>,
    NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/v2/doctor/visits/pending-visits");
        Roles("Doctor");
    }
    
    
    public override async Task<Results<Ok<List<VisitRequestResponce>>, NotFound, ProblemDetails>> 
        ExecuteAsync(CancellationToken cancellationToken)
    {
        var doctorId = userService.GetUserId(HttpContext.User);
        
        var dto = new DoctorRequestFilterDto 
        { 
            Id = doctorId,
            Status = VisitStatus.Approved
        };

        var listOfPendingRequestsCommand = dto.MapToCommand();
        
        var result = await mediator.Send(listOfPendingRequestsCommand, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}
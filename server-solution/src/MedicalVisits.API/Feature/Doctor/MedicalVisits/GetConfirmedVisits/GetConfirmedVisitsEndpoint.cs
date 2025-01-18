using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits.GetConfirmedVisits;

public class GetConfirmedVisitsEndpoint(IUserService userService, IMediator mediator) : EndpointWithoutRequest<
    Results<Ok<RouteResponse>,
        NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/v2/doctor/visits/confirmed-visits");
        Roles("Doctor");
    }

    public override async Task<Results<Ok<RouteResponse>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        
        var doctorId = userService.GetUserId(HttpContext.User);
        
        var command = new GetConfirmVisitRequestsCommand(doctorId);

        var result =  await mediator.Send(command, ct);
        
        return TypedResults.Ok(result);
    }
}
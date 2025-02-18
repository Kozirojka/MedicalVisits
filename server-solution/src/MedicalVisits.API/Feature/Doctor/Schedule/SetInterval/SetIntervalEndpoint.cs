using FastEndpoints;
using MediatR;
using MedicalVisits.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.SetInterval;

public sealed record SetIntervalRequest(DateTime StartDate, DateTime EndDate, int? VisitRequestId);

public class SetIntervalEndpoint(IMediator mediator, IUserService service) : Endpoint<SetIntervalRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{
    
    public override void Configure()
    {
        Post("/api/doctor/interval");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetIntervalRequest req, CancellationToken ct)
    {
        var doctorId = service.GetUserId(HttpContext.User);
         
        var command = req.ToCommand(doctorId);
    
        var result = await mediator.Send(command, ct);
        
        return TypedResults.Ok(true);
    }
}
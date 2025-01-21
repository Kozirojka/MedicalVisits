using FastEndpoints;
using MediatR;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.v2.SetInterval;

public sealed record SetIntervalRequest(List<DoctorIntervals> DoctorIntervals);

public class SetIntervalEndpoint(IMediator mediator) : Endpoint<SetIntervalRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{

    public override void Configure()
    {
        Post("/api/admin/set-interval");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetIntervalRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
    
        var result = await mediator.Send(command, ct);
        
        return TypedResults.Ok(true);
    }
}
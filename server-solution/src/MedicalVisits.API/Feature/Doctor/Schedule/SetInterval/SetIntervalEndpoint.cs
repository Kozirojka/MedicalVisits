using System.Text.Json.Serialization;
using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using MedicalVisits.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.SetInterval;

public sealed record SetIntervalRequest(
    [property: JsonPropertyName("startData")] DateTime StartDate,
    [property: JsonPropertyName("endData")] DateTime EndDate,
    int VisitRequestId
);

public class SetIntervalEndpoint(IMediator mediator, IUserService service) : Endpoint<SetIntervalRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{
    
    public override void Configure()
    {
        Post("/api/doctor/interval");
    }

    //todo: проблема тут у тому, що воно не номрально передає час та інші дані
    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetIntervalRequest req, CancellationToken ct)
    {
        var doctorId = service.GetUserId(HttpContext.User);
        var commandToCreateInterval = req.ToCommand(doctorId);
        var result = await mediator.Send(commandToCreateInterval, ct);
        

        
        return TypedResults.Ok(true);
    }
}
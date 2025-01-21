using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Command.SetDoctorSchedule;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Admin.Schedule.SetDoctorSchedule;

public sealed record SetDoctorScheduleRequest(string DoctorId, DateTime StartDate, DateTime EndDate);



public class SetDoctorScheduleEndpoint(IMediator _mediator) : Endpoint<SetDoctorScheduleRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{
    
    
    public override void Configure()
    {
        Post("/api/admin/schedule/setDoctorSchedule");
        Roles("Admin");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetDoctorScheduleRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        
        var result = await _mediator.Send(command, ct);
        
        if (result == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(result);
    }
}
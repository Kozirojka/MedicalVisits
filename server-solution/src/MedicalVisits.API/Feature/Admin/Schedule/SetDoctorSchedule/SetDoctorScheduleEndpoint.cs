using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Command.SetDoctorSchedule;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Admin.Schedule.SetDoctorSchedule;

public sealed record SetDoctorScheduleRequest(List<DoctorSchedules> SchedulesList);



public class SetDoctorScheduleEndpoint(IMediator mediator) : Endpoint<SetDoctorScheduleRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{
    
    
    public override void Configure()
    {
        Post("/api/admin/schedule/setDoctorSchedule");
        Roles("Admin");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetDoctorScheduleRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        
        var result = await mediator.Send(command, ct);
        
        if (result == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(result);
    }
}
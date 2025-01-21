using Azure;
using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Command.CreateScheduleWithSlots;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Dtos.Schedule;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.SetSchedultAndSlots;

public class SetScheduleAndSlotsEndpoint(IUserService userService, IMediator mediator) : Endpoint<ScheduleRequest, 
    Results<Ok<bool>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/api/doctor/set/schedule-slots");
        Roles("Doctor");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(ScheduleRequest req, CancellationToken ct)
    {
        var doctorId = userService.GetUserId(HttpContext.User);
    
        var command = new CreateScheduleWithSlotsCommand(req, doctorId);
       
        var result = await mediator.Send(command, ct);

        if (!result)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(result);
        
    }
}
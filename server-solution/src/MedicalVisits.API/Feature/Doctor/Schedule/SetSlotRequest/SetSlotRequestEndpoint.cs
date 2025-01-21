using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Command.AddSlotToSchedule;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Dtos.Schedule;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.SetSlotRequest;

public sealed record SetSlotRequest(List<TimeSlotDto> Slots,
    int Schedule);

public class SetSlotRequestEndpoint(IUserService userService, IMediator mediator) : Endpoint<SetSlotRequest, Results<Ok<bool>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/api/doctor/add-time-slot");
    }

    public override async Task<Results<Ok<bool>, NotFound, ProblemDetails>> ExecuteAsync(SetSlotRequest req, CancellationToken ct)
    {
        var doctorId = userService.GetUserId(HttpContext.User);

        var command = new AddSlotScheduleCommand(req.Slots, req.Schedule);
        
        var result = await mediator.Send(command, ct);
        
        
        return TypedResults.Ok(result);

    }
}
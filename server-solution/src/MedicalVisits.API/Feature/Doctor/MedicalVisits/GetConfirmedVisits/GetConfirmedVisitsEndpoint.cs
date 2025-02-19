using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.diraction;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits.GetConfirmedVisits;
public class GetConfirmedVisitsRequest
{
    public DateTime SelectedDay { get; set; }
}

public class GetConfirmedVisitsEndpoint(IUserService userService, IMediator mediator) : Endpoint<GetConfirmedVisitsRequest,
    Results<Ok<RouteResponse>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/v2/doctor/visits/confirmed-visits/{selectedDay}");
        Roles("Doctor");
    }

    public override async Task<Results<Ok<RouteResponse>, NotFound, ProblemDetails>> ExecuteAsync(GetConfirmedVisitsRequest selectedDay, CancellationToken ct)
    {
        Console.WriteLine($"Отримано selectedDay: '{selectedDay.SelectedDay}'");
    
    
        var doctorId = userService.GetUserId(HttpContext.User);
        if (string.IsNullOrEmpty(doctorId))
        {
            return TypedResults.NotFound();
        }
    
        var command = new GetConfirmVisitRequestsCommand(doctorId, selectedDay.SelectedDay);
        var result = await mediator.Send(command, ct);
    
        if (result is null)
        {
            return TypedResults.NotFound();
        }
    
        return TypedResults.Ok(result);
    }

}
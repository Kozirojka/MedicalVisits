using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Queries.GetIntervals;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.Schedule.GetIntervals;

public class GetIntervals(IMediator _mediator, IUserService userService) : EndpointWithoutRequest<Results<Ok<List<DoctorIntervals>>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
            Get("/api/doctor/intervals");
        Roles("Doctor");
    }

    public override async Task<Results<Ok<List<DoctorIntervals>>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken ct)
    {
        var doctorId = userService.GetUserId(HttpContext.User);

        var query = new GetIntervalsQuery(doctorId);
        var intervals = await _mediator.Send(query, ct);

        if (intervals.Count == 0)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(intervals);
    }
}
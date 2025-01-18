using System.Security.Claims;
using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Doctor.MedicalVisits.AssignDoctorToVisit;



public class AssignDoctorToVisitEndpoint(IMediator mediator) : EndpointWithoutRequest<
    Results<Ok<AssignmentResult>,
    NotFound,
    ProblemDetails>>
{
    public override void Configure()
    {
        Put("/api/doctor/assign/{visitId}/{scheduleId}");
        Roles("Doctor");
    }

    public override async Task<Results<Ok<AssignmentResult>,
        NotFound, ProblemDetails>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var visitRequestId = Route<int>("visitId");
        var scheduleId = Route<int>("scheduleId");
        
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new AssignDoctorToVisitCommand(visitRequestId, doctorId, scheduleId);

        
        var result = await mediator.Send(command, cancellationToken);

        return TypedResults.Ok(result);
    }
}
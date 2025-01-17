using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Queries.GetNearestDoctors;
using MedicalVisits.Models.diraction;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Admin.FindNearestDoctor;

public class GetNearestDoctorsEndpoint(IMediator _mediator) : EndpointWithoutRequest<
    Results<Ok<List<DoctorProfileWithDistance>>, 
        NotFound,
        ProblemDetails>>
{
    public override void Configure()
    {
        Get("/api/admin/doctors/nearest/{RequestId}"); 
        Roles("Admin");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var requestId = Route<int>("RequestId");

        if (requestId == null)
        {
            await SendAsync(TypedResults.NotFound());
            return;
        }
        
        var query = new GetNearestDoctorsQuery(requestId);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null || result.Count == 0)
        {
            await SendAsync(TypedResults.NotFound());
            return;
        }

        await SendAsync(TypedResults.Ok(result));
    }
}   
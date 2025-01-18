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
        Get("/api/admin/doctors/nearest/{VisitRequestId}"); 
        Roles("Admin");
    }

    public override async Task<Results<Ok<List<DoctorProfileWithDistance>>, 
        NotFound, ProblemDetails>> 
        ExecuteAsync(CancellationToken cancellationToken)
    {
        var requestId = Route<int>("VisitRequestId");

        if (requestId == null)
        {
            return TypedResults.NotFound();
        }
        
        var query = new GetNearestDoctorsQuery(requestId);
        var result = await _mediator.Send(query, cancellationToken);
        
        if (result == null || result.Count == 0)
        {
            
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(result);
    }
}   
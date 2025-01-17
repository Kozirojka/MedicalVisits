using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Command.AttachVisitToDoctor;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.API.Feature.Admin.AttachVisitResultToDoctor;


public sealed record VisitDoctorInfoDto(string DoctorId, int VisitId);




public class AttachVisitResultToDoctorEndpoint(IMediator _mediator) : Endpoint<VisitDoctorInfoDto, Results<Ok<ResultOfAttach>,
    NotFound, ProblemDetails>>
{

    public override void Configure()
    {
        Post("api/admin/visit-requests/attach");
        Roles("Admin");
    }

    public override async Task<Results<Ok<ResultOfAttach>, NotFound, ProblemDetails>> ExecuteAsync(VisitDoctorInfoDto req, CancellationToken ct)
    {
        
        var command = new AttachVisitToDoctorCommand()
        {
            DoctorId = req.DoctorId,
            VisitRequestId = req.VisitId
        };
        
        ResultOfAttach result = await _mediator.Send(command);
        
        
        if (result == null)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(result);
    }
}
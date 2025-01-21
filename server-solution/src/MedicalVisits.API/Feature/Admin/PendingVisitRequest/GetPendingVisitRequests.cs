using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Queries.FindAppendingRequests;

namespace MedicalVisits.API.Feature.Admin.VisitRequest;

public sealed record ListOfUsers(List<VisitResponceDto> Visit);

public class GetPendingVisitRequests(IMediator mediator) : EndpointWithoutRequest<ListOfUsers>
{
    public override void Configure()
    {
        Get("/api/v2/admin/visit-requests");
        Roles("Admin");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new FindAppendingRequestsQuery();
        var result = await mediator.Send(command, ct);

        if (result == null || result.Count == 0)
        {
            await SendAsync(null, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await SendOkAsync(new ListOfUsers(result), ct);
    }
}
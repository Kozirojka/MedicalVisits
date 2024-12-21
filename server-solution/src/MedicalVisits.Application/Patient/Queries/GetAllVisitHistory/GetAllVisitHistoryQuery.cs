using MediatR;
using MedicalVisits.Application.Admin.Queries.FindAppendingRequests;

namespace MedicalVisits.Application.Patient.Queries.GetAllVisitHistory;

public class GetAllVisitHistoryQuery : IRequest<List<VisitResponceDto>>
{
    public GetAllVisitHistoryQuery(string? userId)
    {
        UserId = userId;
    }

    public string? UserId { get; set; }
}

using MediatR;
using MedicalVisits.Application.Admin.Queries.FindAppendingRequests;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Application.Patient.Queries.GetAllVisitHistory;

public class GetAllVisitHistoryQueryHandler : IRequestHandler<GetAllVisitHistoryQuery, List<VisitResponceDto>>
{
    
    private readonly ApplicationDbContext _context;
    
    public GetAllVisitHistoryQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VisitResponceDto>> Handle(GetAllVisitHistoryQuery request,
        CancellationToken cancellationToken)
    {

        var visitHistory = _context.VisitRequests.Where(u => u.Patient.Id == request.UserId)
            .Where(u => u.Status == VisitStatus.Completed);


        if (visitHistory == null)
        {
            return null;
        }

        var proection = visitHistory.Select(visit => new VisitResponceDto()
        {
            PatienId = visit.PatientId,
            Id = visit.Id,
            Description = visit.Description,
            DateTimeStart = visit.DateTime,
            DateTimeEnd = visit.DateTimeEnd
        }).ToList();
        
        return proection;
}
}

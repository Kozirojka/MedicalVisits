using MediatR;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetNearestDoctorsQuery : IRequest<List<DoctorProfileWithDistance>>
{
    
    //here parameter is id of VisitRequest
    public GetNearestDoctorsQuery(int visitId)
    {
        requestId = visitId;
    }
    
    public int requestId { get; set; }
    
}

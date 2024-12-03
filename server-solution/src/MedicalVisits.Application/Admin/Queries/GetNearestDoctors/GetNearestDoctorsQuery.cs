using MediatR;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetNearestDoctorsQuery : IRequest<List<GetNearestDoctorsQueryHandler.DoctorProfileWithDistance>>
{
    public GetNearestDoctorsQuery(int address)
    {
        requestId = address;
    }
    
    public int requestId { get; set; }
    
}

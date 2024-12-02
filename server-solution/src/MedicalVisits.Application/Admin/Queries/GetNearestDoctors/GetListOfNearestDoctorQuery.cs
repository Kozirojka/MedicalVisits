using MediatR;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetListOfNearestDoctorQuery : IRequest<List<GetListOfNearestDoctorQueryHandler.DoctorProfileWithDistance>>
{
    public GetListOfNearestDoctorQuery(int address)
    {
        requestId = address;
    }
    
    public int requestId { get; set; }
    
}

using MediatR;
using MedicalVisits.Models.Dtos;

namespace MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;

public class GetPendingRequestsForDoctorCommand : IRequest<List<VisitRequestResponce>>
{
    public GetPendingRequestsForDoctorCommand(DoctorRequestFilterDto doctor)
    {
        Doctor = doctor;
    }
    
    public DoctorRequestFilterDto Doctor { get; set; }
    
}




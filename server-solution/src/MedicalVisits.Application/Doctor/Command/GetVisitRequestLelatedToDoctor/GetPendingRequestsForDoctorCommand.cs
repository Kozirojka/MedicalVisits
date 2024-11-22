using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Doctor.Command.GetVisitRequestLelatedToDoctor;

public class GetPendingRequestsForDoctorCommand : IRequest<List<VisitRequestDtoNew>>
{
    public GetPendingRequestsForDoctorCommand(DoctorRequestFilterDto doctor)
    {
        Doctor = doctor;
    }


    public DoctorRequestFilterDto Doctor { get; set; }
    
}




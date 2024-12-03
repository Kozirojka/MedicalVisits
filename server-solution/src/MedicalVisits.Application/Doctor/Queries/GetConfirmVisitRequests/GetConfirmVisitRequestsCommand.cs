using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommand : IRequest<MedicalVisits.Models.diraction.RouteResponse>
{
    public GetConfirmVisitRequestsCommand(string doctorId)
    {
        DoctorId = doctorId;
    }

    public string DoctorId;
    public VisitStatus status = VisitStatus.Approved;
}

using MediatR;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommand : IRequest<RouteResponse>
{
    public GetConfirmVisitRequestsCommand(string doctorId)
    {
        DoctorId = doctorId;
    }

    public readonly string DoctorId;
    public readonly VisitStatus Status = VisitStatus.Approved;
}

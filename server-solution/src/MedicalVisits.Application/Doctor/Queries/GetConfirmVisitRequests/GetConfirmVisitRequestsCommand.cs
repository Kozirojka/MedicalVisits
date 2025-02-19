using MediatR;
using MedicalVisits.Models.diraction;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;

public class GetConfirmVisitRequestsCommand(string doctorId, DateTime selectedDay) : IRequest<RouteResponse>
{
    public readonly string DoctorId = doctorId;
    public readonly VisitStatus Status = VisitStatus.Approved;
    public readonly DateTime SelectedDay = selectedDay;
}

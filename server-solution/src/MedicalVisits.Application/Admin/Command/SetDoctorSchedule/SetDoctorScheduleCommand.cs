using MediatR;
namespace MedicalVisits.Application.Admin.Command.SetDoctorSchedule;

public sealed record SetDoctorScheduleCommand : IRequest<bool>
{
    public string DoctorId {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
}
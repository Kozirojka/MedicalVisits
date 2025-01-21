using MediatR;
using MedicalVisits.Models.Entities.ScheduleV2;

namespace MedicalVisits.Application.Admin.Command.SetDoctorSchedule;

public sealed record SetDoctorScheduleCommand : IRequest<bool>
{
    public List<DoctorSchedules> DoctorSchedules { get; set; } = new();
}
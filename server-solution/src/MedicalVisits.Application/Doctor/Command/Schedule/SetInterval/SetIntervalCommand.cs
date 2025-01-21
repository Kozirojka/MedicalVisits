using MediatR;
using MedicalVisits.Models.Entities.ScheduleV2;

namespace MedicalVisits.Application.Doctor.Command.Schedule.SetInterval;

public class SetIntervalCommand : IRequest<bool>
{
    public List<DoctorIntervals> _doctorIntervals;
    public SetIntervalCommand(List<DoctorIntervals> doctorIntervals)
    {
        _doctorIntervals = doctorIntervals;
    }
}
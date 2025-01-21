using MedicalVisits.Application.Admin.Command.SetDoctorSchedule;

namespace MedicalVisits.API.Feature.Admin.Schedule.SetDoctorSchedule;

public static class SetDoctorScheduleMappingExtensions
{
    public static SetDoctorScheduleCommand ToCommand(this SetDoctorScheduleRequest request)
    {
        return new SetDoctorScheduleCommand
        {
            DoctorSchedules = request.SchedulesList
        };
    }
}
using MedicalVisits.Application.Admin.Command.SetDoctorSchedule;

namespace MedicalVisits.API.Feature.Admin.Schedule.SetDoctorSchedule;

public static class SetDoctorScheduleMappingExtensions
{
    public static SetDoctorScheduleCommand ToCommand(this SetDoctorScheduleRequest request)
    {
        return new SetDoctorScheduleCommand
        {
            DoctorId = request.DoctorId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
    }
}
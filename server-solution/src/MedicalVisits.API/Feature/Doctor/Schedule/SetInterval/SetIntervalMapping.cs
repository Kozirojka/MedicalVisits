using MedicalVisits.API.Feature.Doctor.Schedule.v2.SetInterval;
using MedicalVisits.Application.Doctor.Command.Schedule.SetInterval;

namespace MedicalVisits.API.Feature.Doctor.Schedule.v2;

public static class SetIntervalMapping
{
    public static SetIntervalCommand ToCommand(this SetIntervalRequest request)
    {
        return new SetIntervalCommand(request.DoctorIntervals);
    }
}
﻿using MedicalVisits.Application.Doctor.Command.Schedule.SetInterval;

namespace MedicalVisits.API.Feature.Doctor.Schedule.SetInterval;

public static class SetIntervalMapping
{
    public static SetIntervalCommand ToCommand(this SetIntervalRequest request)
    {
        return new SetIntervalCommand(request.DoctorIntervals);
    }
}
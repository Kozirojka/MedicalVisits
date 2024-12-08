namespace MedicalVisits.Models.Dtos.Schedule.CreateScheduleDto;

public class WorkingHoursRange
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
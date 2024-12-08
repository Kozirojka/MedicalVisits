namespace MedicalVisits.Models.Dtos.Schedule.CreateScheduleDto;

public class DayScheduleRequest
{
    public DayOfWeek DayOfWeek { get; set; }
    public List<WorkingHoursRange> WorkingHours { get; set; }
}
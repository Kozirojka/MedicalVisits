namespace MedicalVisits.Models.Dtos.Schedule.CreateScheduleDto;

public class CreateScheduleRequest
{
    public string? DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan SlotDuration { get; set; }
    public List<DayScheduleRequest> DaySchedules { get; set; }
}

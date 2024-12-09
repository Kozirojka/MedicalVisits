namespace MedicalVisits.Models.Dtos.Schedule;

public class TimeSlotDto()
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; }
}

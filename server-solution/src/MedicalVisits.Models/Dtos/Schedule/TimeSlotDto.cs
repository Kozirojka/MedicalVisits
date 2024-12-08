namespace MedicalVisits.Models.Dtos.Schedule;

public class TimeSlotDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
}

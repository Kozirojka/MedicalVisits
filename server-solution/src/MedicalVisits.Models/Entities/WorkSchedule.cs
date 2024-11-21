namespace MedicalVisits.Models.Entities;

public class WorkSchedule
{
    public int Id { get; set; }

    public int DoctorProfileId { get; set; }
    public DoctorProfile Doctor { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public bool IsHoliday { get; set; } = false; 
}
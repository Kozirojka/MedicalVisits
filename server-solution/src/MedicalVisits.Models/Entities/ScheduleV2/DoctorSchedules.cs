namespace MedicalVisits.Models.Entities.ScheduleV2;

public class DoctorSchedules
{
    public int Id { get; set; }
    
    public DoctorProfile? Doctor { get; set; }
    public int DoctorId { get; set; }
    
    public DayOfWeek DayOfWeek { get; set; }
    
    public int MinimumAppointments { get; set; }
    
    public bool? IsAvailable { get; set; }
}
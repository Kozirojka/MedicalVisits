namespace MedicalVisits.Models.Entities.ScheduleV2;

public class DoctorIntervals
{
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public DoctorProfile Doctor { get; set; }
    
    public DateTime StartInterval { get; set; }
    public DateTime EndInterval { get; set; }
    
    public DoctorSchedules DoctorSchedules { get; set; }
    public int DoctorScheduleId { get; set; }
}
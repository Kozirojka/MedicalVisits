
using MedicalVisits.Models.Entities.Schedule;

namespace MedicalVisits.Models.Entities;

public class DoctorProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Specialization { get; set; }
    public string LicenseNumber { get; set; }
    
    public ICollection<DoctorWorkSchedule> WorkSchedules { get; set; }
    public ICollection<VisitRequest>? visitRequests { get; set; }
    
    public DoctorProfile() 
    {
        WorkSchedules = new List<DoctorWorkSchedule>();
        visitRequests = new List<VisitRequest>();
    }

    
}
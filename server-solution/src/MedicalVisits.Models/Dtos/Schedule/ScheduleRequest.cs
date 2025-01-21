using MedicalVisits.Models.Entities.Schedule;

namespace MedicalVisits.Models.Dtos.Schedule;

public class ScheduleRequest()
{
    public List<TimeSlotDto> _timeSlots { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Name { get; set; }
}
using MedicalVisits.Models.Enums;

namespace MedicalVisits.Models.Entities.Schedule;

public class TimeSlot
{
    public int Id { get; private set; }
    public int ScheduleId { get; private set; }
    public DoctorWorkSchedule Schedule { get; private set; }
    
    public DateTime Date { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public TimeSlotStatus Status { get; set; }
    
    public VisitRequest AssignedVisit { get; private set; }
    public int? VisitRequestId { get; set; }
    
    
    
    protected TimeSlot() { }

    public static TimeSlot Create(
        int scheduleId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan duration)
    {
        return new TimeSlot
        {
            ScheduleId = scheduleId,
            Date = date,
            StartTime = startTime,
            Duration = duration,
            Status = TimeSlotStatus.Available
        };
    }

    public bool CanAcceptVisit()
    {
        return Status == TimeSlotStatus.Available && AssignedVisit == null;
    }

    public void AssignVisit(VisitRequest visit)
    {
        if (!CanAcceptVisit())
            throw new InvalidOperationException("Time slot is not available");

        AssignedVisit = visit;
        Status = TimeSlotStatus.Booked;
    }
}
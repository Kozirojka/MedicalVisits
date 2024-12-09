namespace MedicalVisits.Models.Entities.Schedule;

public class TimeSlot
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; }
    
    public int WorkPlanId { get; set; }
    public ScheduleWorkPlan WorkPlan { get; set; } = null!;
    
    public VisitRequest? Request { get; set; }
    public int? RequestId { get; set; }
    
    
    public TimeSlot() { }
    
    public TimeSlot(DateTime startTime, DateTime endTime, VisitRequest visitRequest)
    {
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = true;
        RequestId = visitRequest.Id;
    }

    public void SetVisitRequest(int requestId)
    {
        RequestId = requestId;
    }
    
    
    
}

namespace MedicalVisits.Models.Entities.Schedule;

public class DoctorWorkSchedule
{
    public int Id { get; set; }
    public string DoctorId { get; private set; }
    public ApplicationUser Doctor { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public TimeSpan DefaultStartTime { get; set; }
    public TimeSpan DefaultEndTime { get; set; }
    
    public ICollection<TimeSlot>? TimeSlots { get; set; }
    
    protected DoctorWorkSchedule()
    {
        TimeSlots = new List<TimeSlot>();
    }
    
    public static DoctorWorkSchedule Create(
        string doctorId, 
        DateTime startDate, 
        DateTime endDate,
        TimeSpan defaultStartTime,
        TimeSpan defaultEndTime)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date");

        return new DoctorWorkSchedule
        {
            DoctorId = doctorId,
            StartDate = startDate,
            EndDate = endDate,
            DefaultStartTime = defaultStartTime,
            DefaultEndTime = defaultEndTime
        };
    }

    public void GenerateTimeSlots(TimeSpan slotDuration, DayOfWeek[] workDays)
    {
        var currentDate = StartDate;
        while (currentDate <= EndDate)
        {
            if (workDays.Contains(currentDate.DayOfWeek))
            {
                var currentTime = DefaultStartTime;
                while (currentTime.Add(slotDuration) <= DefaultEndTime)
                {
                    TimeSlots.Add(TimeSlot.Create(
                        Id,
                        currentDate,
                        currentTime,
                        slotDuration
                    ));
                    currentTime = currentTime.Add(slotDuration);
                }
            }
            currentDate = currentDate.AddDays(1);
        }
    }
    
}

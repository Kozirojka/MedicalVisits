using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.Models.Entities.Schedule;

public class ScheduleWorkPlan
{
    public int Id { get; set; }
    public string DoctorId { get; set; } = null!;
    public DoctorProfile Doctor { get; set; } = null!;
    public string NameOfSchedule { get; set; } = null!;
    public ICollection<TimeSlot> TimeSlots { get; private set; }

    private ScheduleWorkPlan()
    {
        TimeSlots = new List<TimeSlot>();
    }

    public ScheduleWorkPlan(DoctorProfile doctor, string nameOfSchedule)
    {
        Doctor = doctor;
        DoctorId = doctor.User.Id;
        NameOfSchedule = nameOfSchedule;
        TimeSlots = new List<TimeSlot>();
    }

    public void SetTimeSlots(List<TimeSlot> timeSlots)
    {
        TimeSlots = timeSlots;
    }

    public void AddTimeSlot(TimeSlot timeSlot)
    {
        TimeSlots.Add(timeSlot);
    }

    public void DeleteTimeSlot(TimeSlot timeSlot)
    {
        TimeSlots.Remove(timeSlot);
    }
}
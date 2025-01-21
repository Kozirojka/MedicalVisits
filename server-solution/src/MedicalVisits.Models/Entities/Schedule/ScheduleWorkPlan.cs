using Microsoft.AspNetCore.Http.HttpResults;

namespace MedicalVisits.Models.Entities.Schedule;

/// <summary>
/// <para style="color:blue">🏥 Модель розкладу роботи лікаря</para>
/// </summary>
/// <remarks>
/// Визначає основні відношення в системі планування:
/// <list type="bullet">
///     <item>
///         <term>👨‍⚕️ Лікар → Розклад</term>
///         <description>Відношення один-до-одного (1:1). Кожен лікар має унікальний розклад роботи.</description>
///     </item>
///     <item>
///         <term>📅 Розклад → Часові слоти</term>
///         <description>Відношення один-до-багатьох (1:N). Розклад містить множину часових слотів.</description>
///     </item>
/// </list>
/// </remarks>
/// <seealso cref="TimeSlot"/>
/// <seealso cref="Doctor"/>
public class ScheduleWorkPlan
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public string NameOfSchedule { get; set; } = null!;
    public ICollection<TimeSlot> TimeSlots { get; set; }
    
    public ScheduleWorkPlan()
    {
        TimeSlots = new List<TimeSlot>();
    }

    public ScheduleWorkPlan(ApplicationUser doctor, string nameOfSchedule)
    {
        User = doctor;
        UserId = doctor.Id;   
        NameOfSchedule = nameOfSchedule;
        TimeSlots = new List<TimeSlot>();
    }


    public void SetTimeSlots(List<TimeSlot> timeSlots)
    {
        TimeSlots = timeSlots;
    }

    public void AddTimeSlot(List<TimeSlot> timeSlots)
    {
        foreach (var slot in timeSlots)
        {
            TimeSlots.Add(slot);
        }
    }
    
    public void DeleteTimeSlot(TimeSlot timeSlot)
    {
        TimeSlots.Remove(timeSlot);
    }
}
namespace MedicalVisits.Models.Dtos.DoctorDto;

public class DoctorScheduleRequestDto
{
        public string? DoctorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan SlotDuration { get; set; }
        public DayOfWeek[] WorkDays { get; set; }
}

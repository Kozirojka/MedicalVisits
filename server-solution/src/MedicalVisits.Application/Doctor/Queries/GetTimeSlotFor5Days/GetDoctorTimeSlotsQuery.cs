using MediatR;
using MedicalVisits.Models.Dtos.Schedule;

namespace MedicalVisits.Application.Doctor.Queries.GetTimeSlotFor5Days;

public class GetDoctorTimeSlotsQuery : IRequest<List<TimeSlotDto>>
{
    public string DoctorId { get; set; }
    public DateTime StartDate { get; set; }
}



using MediatR;
using MedicalVisits.Models.Dtos.Schedule;

namespace MedicalVisits.Application.Doctor.Command.AddSlotToSchedule;


public class AddSlotScheduleCommnad : IRequest<bool>
{
    
    
    public List<TimeSlotDto> _slotDtos;
    public int ScheduleId { get; set; }
    
    public AddSlotScheduleCommnad(List<TimeSlotDto> slotDtos, int scheduleId)
    {
        _slotDtos = slotDtos;
        ScheduleId = scheduleId;
    }
}

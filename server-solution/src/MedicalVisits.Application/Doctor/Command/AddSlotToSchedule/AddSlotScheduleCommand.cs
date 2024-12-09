using MediatR;
using MedicalVisits.Models.Dtos.Schedule;

namespace MedicalVisits.Application.Doctor.Command.AddSlotToSchedule;


public class AddSlotScheduleCommand : IRequest<bool>
{
    
    
    public List<TimeSlotDto> _slotDtos;
    public int ScheduleId { get; set; }
    
    public AddSlotScheduleCommand(List<TimeSlotDto> slotDtos, int scheduleId)
    {
        _slotDtos = slotDtos;
        ScheduleId = scheduleId;
    }
}

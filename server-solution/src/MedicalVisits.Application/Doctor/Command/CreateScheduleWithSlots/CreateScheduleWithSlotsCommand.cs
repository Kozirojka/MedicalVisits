using MediatR;
using MedicalVisits.Models.Dtos.Schedule;

namespace MedicalVisits.Application.Doctor.Command.CreateScheduleWithSlots;

public class CreateScheduleWithSlotsCommand : IRequest<bool>
{

    public CreateScheduleWithSlotsCommand(ScheduleRequestDto scheduleRequestDto, string DoctorId)
    {
        ScheduleRequestDto = scheduleRequestDto;
        this.DoctorId = DoctorId;
    }
        
    
    public string DoctorId { get; init; }
    public ScheduleRequestDto ScheduleRequestDto { get; set; }
    
}

using MediatR;
using MedicalVisits.Models.Dtos.Schedule;

namespace MedicalVisits.Application.Doctor.Command.CreateScheduleWithSlots;

public class CreateScheduleWithSlotsCommand : IRequest<bool>
{

    public CreateScheduleWithSlotsCommand(ScheduleRequest scheduleRequest, string DoctorId)
    {
        ScheduleRequest = scheduleRequest;
        this.DoctorId = DoctorId;
    }
        
    
    public string DoctorId { get; init; }
    public ScheduleRequest ScheduleRequest { get; set; }
    
}

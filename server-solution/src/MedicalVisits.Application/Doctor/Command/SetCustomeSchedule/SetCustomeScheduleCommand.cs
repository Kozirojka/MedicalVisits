using MediatR;
using MedicalVisits.Models.Dtos.Schedule.CreateScheduleDto;

namespace MedicalVisits.Application.Doctor.Command.SetCustomeSchedule;

public class SetCustomeScheduleCommand : IRequest<bool>
{

    public SetCustomeScheduleCommand(CreateScheduleRequest schedule)
    {
        Schedule = schedule;
    }
    
    
    public CreateScheduleRequest Schedule { get; set; }
}

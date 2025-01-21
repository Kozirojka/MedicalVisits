using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities.ScheduleV2;

namespace MedicalVisits.Application.Admin.Command.SetDoctorSchedule;

public class SetDoctorScheduleCommandHandler : IRequestHandler<SetDoctorScheduleCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public SetDoctorScheduleCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(SetDoctorScheduleCommand request, CancellationToken cancellationToken)
    {
        
       _dbContext.DoctorSchedules.AddRange(request.DoctorSchedules);
        
       var result = await _dbContext.SaveChangesAsync(cancellationToken);

       if (result < 0)
       {
            return false;           
       }

       return true;
    }
}
using MediatR;
using MedicalVisits.Infrastructure.Persistence;

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
        
        
        return true;
    }
}
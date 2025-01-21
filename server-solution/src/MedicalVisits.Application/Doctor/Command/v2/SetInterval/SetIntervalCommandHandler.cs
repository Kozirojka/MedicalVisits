using MediatR;
using MedicalVisits.Infrastructure.Persistence;

namespace MedicalVisits.Application.Doctor.Command.v2.SetInterval;

public class SetIntervalCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<SetIntervalCommand, bool>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<bool> Handle(SetIntervalCommand request, CancellationToken cancellationToken)
    {
         _dbContext.DoctorIntervals.AddRange(request._doctorIntervals);
        
         int result = _dbContext.SaveChanges();
         if (result < 0)
         {
             return Task.FromResult(false);
         }
            
         return Task.FromResult(true);
    }
}
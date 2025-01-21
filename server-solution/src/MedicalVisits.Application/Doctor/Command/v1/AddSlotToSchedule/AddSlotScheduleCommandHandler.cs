using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Doctor.Command.AddSlotToSchedule;

public class AddSlotScheduleCommandHandler : IRequestHandler<AddSlotScheduleCommand,bool>
{
    private readonly ILogger<AddSlotScheduleCommandHandler> _logger;
    
    private readonly ApplicationDbContext _dbContext;

    public AddSlotScheduleCommandHandler(ApplicationDbContext dbContext, ILogger<AddSlotScheduleCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<bool> Handle(AddSlotScheduleCommand request, CancellationToken cancellationToken)
    {

        if (request._slotDtos == null)
        {
            _logger.LogError("Invalid request");
            return false;
        }

        var schedule = await _dbContext.ScheduleWorkPlans
            .SingleOrDefaultAsync(u => u.Id == request.ScheduleId, cancellationToken: cancellationToken);
        
        List<TimeSlot> timeSlots = request._slotDtos
            .Select(ts => new TimeSlot
            {
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                IsAvailable = ts.IsAvailable
            }).ToList();

        
        schedule?.AddTimeSlot(timeSlots);


        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result < 0)
        {
            _logger.LogError("Failed to add slot");
            return false;
        }
        
        return true;
    }
}
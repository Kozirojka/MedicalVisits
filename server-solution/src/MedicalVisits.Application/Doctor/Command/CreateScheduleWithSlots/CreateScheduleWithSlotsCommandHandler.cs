using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.Schedule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Doctor.Command.CreateScheduleWithSlots;

public class CreateScheduleWithSlotsCommandHandler : IRequestHandler<CreateScheduleWithSlotsCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CreateScheduleWithSlotsCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public CreateScheduleWithSlotsCommandHandler(ApplicationDbContext dbContext, ILogger<CreateScheduleWithSlotsCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateScheduleWithSlotsCommand request, CancellationToken cancellationToken)
{
    var doctorId = await _dbContext.DoctorProfiles
        .AsNoTracking()
        .Where(u => u.User.Id == request.DoctorId)
        .Select(d => d.Id)
        .SingleOrDefaultAsync(cancellationToken);
        
    var userId = await _dbContext
        .DoctorProfiles.
        Where(u => u.Id == doctorId)
        .Select(u => u.User.Id)
        .SingleOrDefaultAsync(cancellationToken);
    
    
    
    if (doctorId == 0)
    {
        _logger.LogError($"Doctor with ID {request.DoctorId} not found.");
        return false;
    }

    if (request.ScheduleRequest._timeSlots == null || !request.ScheduleRequest._timeSlots.Any())
    {
        _logger.LogWarning("No time slots provided for the schedule.");
        return false;
    }

    if (request.ScheduleRequest._timeSlots.Any(ts => ts.StartTime >= ts.EndTime))
    {
        _logger.LogError("Invalid time slots provided: StartTime must be earlier than EndTime.");
        return false;
    }
    
    List<TimeSlot> timeSlots = request.ScheduleRequest._timeSlots
        .Select(ts => new TimeSlot
        {
            StartTime = ts.StartTime,
            EndTime = ts.EndTime,
            IsAvailable = ts.IsAvailable
        }).ToList();
    
    var doctorSchedule = new ScheduleWorkPlan
    {
        UserId = userId,
        NameOfSchedule = request.ScheduleRequest.Name,
        TimeSlots = timeSlots
    };

    using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    try
    {
        await _dbContext.ScheduleWorkPlans.AddAsync(doctorSchedule, cancellationToken);
        var savedChanges = await _dbContext.SaveChangesAsync(cancellationToken);

        if (savedChanges <= 0)
        {
            _logger.LogError("Failed to save changes to the database.");
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }

        await transaction.CommitAsync(cancellationToken);
        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError($"An error occurred while saving schedule: {ex.Message}");
        await transaction.RollbackAsync(cancellationToken);
        return false;
    }
}

}

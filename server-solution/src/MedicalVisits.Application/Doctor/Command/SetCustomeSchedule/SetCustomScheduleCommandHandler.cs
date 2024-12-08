using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.Schedule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Doctor.Command.SetCustomeSchedule;

public class SetCustomScheduleCommandHandler : IRequestHandler<SetCustomeScheduleCommand, bool>
{
    ILogger<SetCustomScheduleCommandHandler> _logger;
    ApplicationDbContext _dbContext;
    UserManager<ApplicationUser> _userManager;


    public SetCustomScheduleCommandHandler(ILogger<SetCustomScheduleCommandHandler> logger,
        ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userManager = userManager;
    }


    public async Task<bool> Handle(SetCustomeScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var startDateUtc = DateTime.SpecifyKind(request.Schedule.StartDate, DateTimeKind.Utc);
            var endDateUtc = DateTime.SpecifyKind(request.Schedule.EndDate, DateTimeKind.Utc);

            _logger.LogInformation(
                "Handling SetCustomeScheduleCommand for DoctorId: {DoctorId}, StartDate: {StartDate}, EndDate: {EndDate}",
                request.Schedule.DoctorId, startDateUtc, endDateUtc);
            
            foreach (var daySchedule in request.Schedule.DaySchedules)
            {
                _logger.LogInformation("Processing DaySchedule for DayOfWeek: {DayOfWeek}", daySchedule.DayOfWeek);

                foreach (var workingHours in daySchedule.WorkingHours)
                {
                    _logger.LogInformation("Adding WorkingHours from {StartTime} to {EndTime}", 
                        workingHours.StartTime,
                        workingHours.EndTime);

                    var schedule = DoctorWorkSchedule.Create(
                        doctorId: request.Schedule.DoctorId,
                        startDate: startDateUtc, 
                        endDate: endDateUtc,      
                        defaultStartTime: workingHours.StartTime,
                        defaultEndTime: workingHours.EndTime
                    );

                    schedule.GenerateTimeSlots(
                        slotDuration: request.Schedule.SlotDuration,
                        workDays: new[] { daySchedule.DayOfWeek }
                    );

                    _dbContext.DoctorWorkSchedules.Add(schedule);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing SetCustomeScheduleCommand");
            throw;
        }
    }
}

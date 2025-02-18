using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Doctor.Command.Schedule.SetInterval;

public class SetIntervalCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<SetIntervalCommand, bool>
{
    public async Task<bool> Handle(SetIntervalCommand request, CancellationToken cancellationToken)
    {
        var doctorProfile = dbContext.DoctorProfiles.SingleOrDefault(user => user.UserId == request.DoctorId);
        
        if (doctorProfile == null)
        {
            return await Task.FromResult(false);
        }

        var newInterval = new DoctorIntervals()
        {
            Doctor = doctorProfile,
            DoctorId = doctorProfile.Id,
            EndInterval = request.EndInterval,
            StartInterval = request.StartInterval
        };

        if (request.DoctorScheduleId == null)
        {
            var newDoctorSchedule = new DoctorSchedules()
            {
                Doctor = doctorProfile,
                DoctorId = doctorProfile.Id,
                DayOfWeek = request.StartInterval.DayOfWeek,
                MinimumAppointments = 5,
                IsAvailable = true
            };

            dbContext.DoctorSchedules.Add(newDoctorSchedule);
            await dbContext.SaveChangesAsync(cancellationToken);

            newInterval.DoctorScheduleId = newDoctorSchedule.Id;
        }

        dbContext.DoctorIntervals.Add(newInterval);
        int result = await dbContext.SaveChangesAsync(cancellationToken);

        if (result <= 0) 
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}

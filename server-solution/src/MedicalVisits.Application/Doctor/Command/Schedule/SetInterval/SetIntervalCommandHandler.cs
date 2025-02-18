using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalVisits.Application.Doctor.Command.Schedule.SetInterval
{
    public class SetIntervalCommandHandler : IRequestHandler<SetIntervalCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;

        public SetIntervalCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> Handle(SetIntervalCommand request, CancellationToken cancellationToken)
        {
            var doctorProfile = await _dbContext.DoctorProfiles
                .SingleOrDefaultAsync(dp => dp.UserId == request.DoctorId, cancellationToken);

            var searchableDay = await _dbContext.DoctorSchedules
                .SingleOrDefaultAsync(u => u.Time.Date == request.StartInterval.Date, cancellationToken);

            if (doctorProfile == null)
            {
                return false;
            }

            DoctorSchedules doctorSchedule;
            if (searchableDay == null)
            {
                doctorSchedule = new DoctorSchedules
                {
                    Doctor = doctorProfile,
                    DoctorId = doctorProfile.Id,
                    DayOfWeek = request.StartInterval.DayOfWeek,
                    MinimumAppointments = 5,
                    IsAvailable = true,
                    Time = request.StartInterval
                };
                _dbContext.DoctorSchedules.Add(doctorSchedule);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                doctorSchedule = await _dbContext.DoctorSchedules
                    .SingleOrDefaultAsync(u => u.Id == searchableDay.Id, cancellationToken);
                
                if (doctorSchedule == null)
                {
                    return false;
                }
            }

            var newInterval = new DoctorIntervals
            {
                Doctor = doctorProfile,
                DoctorId = doctorProfile.Id,
                DoctorSchedules = doctorSchedule,
                DoctorScheduleId = doctorSchedule.Id,
                EndInterval = request.EndInterval,
                StartInterval = request.StartInterval
            }; 

            _dbContext.DoctorIntervals.Add(newInterval);
            int result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
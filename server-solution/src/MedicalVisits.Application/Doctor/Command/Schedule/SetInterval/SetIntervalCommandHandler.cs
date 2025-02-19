using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.EntityFrameworkCore;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;

namespace MedicalVisits.Application.Doctor.Command.Schedule.SetInterval
{
    public class SetIntervalCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
        : IRequestHandler<SetIntervalCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<bool> Handle(SetIntervalCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var doctorProfile = await _dbContext.DoctorProfiles
                    .SingleOrDefaultAsync(dp => dp.UserId == request.DoctorId, cancellationToken);

                if (doctorProfile == null)
                {
                    return false;
                }

                var existingSchedule = await _dbContext.DoctorSchedules
                    .SingleOrDefaultAsync(schedule => schedule.Time.Date == request.StartInterval.Date, cancellationToken);

                DoctorSchedules doctorSchedule;
                if (existingSchedule == null)
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
                        .SingleOrDefaultAsync(schedule => schedule.Id == existingSchedule.Id, cancellationToken);

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
                    StartInterval = request.StartInterval,
                    EndInterval = request.EndInterval,
                    VisitRequestId = request.VisitRequestId
                    
                };

                var assignDoctorCommand = new AssignDoctorToVisitCommand(
                    request.VisitRequestId,
                    doctorProfile.UserId,
                    doctorSchedule.Id);

                var assignmentResult = await mediator.Send(assignDoctorCommand, cancellationToken);

                _dbContext.DoctorIntervals.Add(newInterval);
                var changesSaved = await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return changesSaved > 0;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}

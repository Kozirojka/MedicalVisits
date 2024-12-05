using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos.DoctorDto;
using MedicalVisits.Models.Entities.Schedule;
using MedicalVisits.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Infrastructure.Services;

public class DoctorScheduleService : IDoctorScheduleService
{
    private readonly ApplicationDbContext _context;

    public DoctorScheduleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorWorkSchedule> SetDoctorSchedule(DoctorScheduleRequestDto request)
    {
        var existingSchedule = await _context.DoctorWorkSchedules
            .Where(s => s.DoctorId == request.DoctorId)
            .Where(s => (s.StartDate <= request.EndDate && s.EndDate >= request.StartDate))
            .FirstOrDefaultAsync();

        if (existingSchedule != null)
        {
            throw new InvalidOperationException("You already have a schedule for this period");
        }

        var schedule = DoctorWorkSchedule.Create(
            doctorId: request.DoctorId,
            startDate: request.StartDate,
            endDate: request.EndDate,
            defaultStartTime: request.StartTime,
            defaultEndTime: request.EndTime
        );

        schedule.GenerateTimeSlots(
            slotDuration: request.SlotDuration,
            workDays: request.WorkDays
        );

        _context.DoctorWorkSchedules.Add(schedule);
        await _context.SaveChangesAsync();

        return schedule;
    }

    public async Task AssignVisitToDoctorSlot(int visitRequestId, int timeSlotId)
    {
        var visitRequest = await  _context.VisitRequests
            .Include(t => t.TimeSlot)
            .FirstOrDefaultAsync(u => u.Id == visitRequestId);

        if (visitRequest == null)
        {
            throw  new  NullReferenceException("Немає такої завдання");
        }
        
        var timeSlot = _context.TimeSlots.Include(ts => ts.Schedule)
            .FirstOrDefault(ts => ts.Id == timeSlotId);

        if (timeSlot == null)
        {
            throw new NullReferenceException("Немає такого слота");
        }

        if (timeSlot.Status != TimeSlotStatus.Available)
        {
            throw new InvalidOperationException("Slot is not available");
        }
        
        if (visitRequest.TimeSlot != null)
            throw new InvalidOperationException("Visit is already assigned to a time slot");
        
        
        var visitStartTime = timeSlot.Date.Add(timeSlot.StartTime);
        var visitEndTime = visitStartTime.Add(timeSlot.Duration);

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            visitRequest.DateTime = visitStartTime;
            visitRequest.DateTimeEnd = visitEndTime;
            visitRequest.Status = VisitStatus.Approved;

            timeSlot.Status = TimeSlotStatus.Booked;

            // Create the association between visit and time slot
            visitRequest.TimeSlotId = timeSlot.Id;
            visitRequest.TimeSlot = timeSlot;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }
}

public interface IDoctorScheduleService
{
    public Task<DoctorWorkSchedule> SetDoctorSchedule(DoctorScheduleRequestDto request);
    public Task AssignVisitToDoctorSlot(int visitRequestId, int timeSlotId);
}
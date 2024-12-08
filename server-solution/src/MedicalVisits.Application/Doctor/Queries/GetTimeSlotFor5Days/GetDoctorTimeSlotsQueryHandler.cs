using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos.Schedule;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Queries.GetTimeSlotFor5Days;

public class GetDoctorTimeSlotsQueryHandler : IRequestHandler<GetDoctorTimeSlotsQuery, List<TimeSlotDto>>
{

    private readonly ApplicationDbContext context;

    public GetDoctorTimeSlotsQueryHandler(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<TimeSlotDto>> Handle(GetDoctorTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        
        var endDate = request.StartDate.AddDays(5);
        
        var slots = await context.TimeSlots
            .Include(ts => ts.Schedule)
            .Where(ts => ts.Schedule.DoctorId == request.DoctorId)
            .Where(ts => ts.Date >= request.StartDate && ts.Date <= endDate)
            .OrderBy(ts => ts.Date)
            .ThenBy(ts => ts.StartTime)
            .Select(ts => new TimeSlotDto
            {
                Id = ts.Id,
                Date = ts.Date,
                StartTime = ts.StartTime,
                Duration = ts.Duration,
                Status = ts.Status.ToString()
            })
            .ToListAsync(cancellationToken);

        return slots;
    }
}

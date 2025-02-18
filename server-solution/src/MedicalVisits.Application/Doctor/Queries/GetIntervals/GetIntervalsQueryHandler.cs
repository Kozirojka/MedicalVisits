using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Doctor.Queries.GetIntervals;

public class GetIntervalsQueryHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)

    : IRequestHandler<GetIntervalsQuery, List<DoctorIntervals>>
{
    
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public Task<List<DoctorIntervals>> Handle(GetIntervalsQuery request, CancellationToken cancellationToken)
    {
        var user = _dbContext.DoctorProfiles.FirstOrDefault(u => u.UserId == request.doctorId);

        if (user == null)
        {
            return null;
        }

        var result = _dbContext.DoctorIntervals.
            Where(dr => dr.Doctor.User.Id == request.doctorId).ToList();
            
        return Task.FromResult(result);
        
    }
}
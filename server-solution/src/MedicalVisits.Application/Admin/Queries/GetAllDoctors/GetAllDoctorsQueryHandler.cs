using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetAllDoctors;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, List<DoctorDto>>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public GetAllDoctorsQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    
    
    public async Task<List<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        
        var patients = await _dbContext.DoctorProfiles
            .Include(d => d.User)
            .Select(d => new DoctorDto
            {
                Id = d.UserId,
                Email = d.User.Email,
                FirstName = d.User.FirstName,
                LastName = d.User.LastName,
                Specialization = d.Specialization
            })
            .ToListAsync(cancellationToken);
        
        
        
        
        return patients;
    }
}

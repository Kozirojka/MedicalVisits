using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, List<PatientDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public GetPatientQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<List<PatientDto>> Handle(
        GetPatientQuery request, 
        CancellationToken cancellationToken)
    {
        
        //проблема в майбутньому тут може бути через те, що я забрав адрему 
        //Проблема того, чому воно не знаходило, це те, що у табличці пацієнти немає ніяких даних
        var patients = await _dbContext.PatientProfiles
            .Include(p => p.User)
            .Select(p => new PatientDto
            {
                Id = p.UserId,
                Email = p.User.Email,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
            })
            .ToListAsync(cancellationToken);


        Console.WriteLine("Patients:" + "---------------------------------------------------------------");

        if (patients.Count == 0)
        {
            Console.WriteLine("Some shit");
        }
        else
        {
            foreach (var p in patients)
                Console.WriteLine(p.ToString());
        }




        return patients;
    }
}



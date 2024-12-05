using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.FindAppendingRequests;

public class FindAppendingRequestsQueryHandler : IRequestHandler<FindAppendingRequestsQuery, List<VisitResponceDto>>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public FindAppendingRequestsQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    
    
    
    /*Проблема полягає у тому, що EF Core не може відслідковувати власні (owned) сутності без їх власника.
      У вашому випадку Address є власною сутністю, оскільки її налаштовано через OwnsOne.
      Коли ви використовуєте запит Select і вибираєте лише частину даних (наприклад, v.Patient.Address), 
      EF Core не має інформації про власника (тобто Patient).
      Ця помилка виникає через те, що власні сутності (Address) не можуть існувати окремо від свого власника (Patient).
      Для вирішення проблеми є кілька підходів:*/
    public async Task<List<VisitResponceDto>> Handle(FindAppendingRequestsQuery requests, CancellationToken cancellationToken)
    {

        var listOfRequests = await _dbContext.VisitRequests
            .Where(p => p.Status == VisitStatus.Pending)
            .Include(v => v.Patient) // Завантажуємо навігаційну властивість Patient
            .ThenInclude(p => p.Address) // Завантажуємо Address з ApplicationUser
            .AsNoTracking()
            .Select(v => new VisitResponceDto
            {
                Id = v.Id,
                Description = v.Description,
                DateTimeStart = v.DateTime,
                DateTimeEnd = v.DateTimeEnd,
                Address = v.Patient.Address, 
                PatienId = v.PatientId
            })
            .ToListAsync(cancellationToken);

        return listOfRequests;
        
    }
}


public class VisitResponceDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public Address Address { get; set; }
    public string PatienId { get; set; }
}


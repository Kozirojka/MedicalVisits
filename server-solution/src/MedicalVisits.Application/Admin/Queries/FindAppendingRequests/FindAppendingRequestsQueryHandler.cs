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
    
    
    
    
    public Task<List<VisitResponceDto>> Handle(FindAppendingRequestsQuery requests, CancellationToken cancellationToken)
    {

        var listOfRequests = _dbContext.VisitRequests
            .Where(p => p.Status == VisitStatus.Pending)
            .Select(v => new VisitResponceDto
            {   
                Id = v.Id,
                Description = v.Description,
                DateTimeStart = v.DateTime,
                DateTimeEnd = v.DateTimeEnd,
                Address = v.Address,
                PatienId = v.PatientId
                
                
            }).ToListAsync(cancellationToken); 
        
        return listOfRequests;
        
    }
}


public class VisitResponceDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string Address { get; set; }
    public string PatienId { get; set; }
}


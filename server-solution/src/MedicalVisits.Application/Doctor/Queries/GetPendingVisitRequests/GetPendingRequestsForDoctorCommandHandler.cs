﻿using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;

public class GetPendingRequestsForDoctorCommandHandler : IRequestHandler<GetPendingRequestsForDoctorCommand, List<VisitRequestResponce>>
{
    
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;
    
    
    public GetPendingRequestsForDoctorCommandHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<List<VisitRequestResponce>> Handle(GetPendingRequestsForDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.VisitRequests
            .Where(u => request.Doctor.Id.Equals(u.DoctorId)) 
            .Where(u => u.Status == request.Doctor.Status)   
            .ToListAsync(cancellationToken);

        var visitRequestDtos = new List<VisitRequestResponce>();

        foreach (var vr in result)
        {
            var patientProfile = await _dbContext.PatientProfiles
                .Include(p => p.User) 
                .ThenInclude(u => u.Address) 
                .FirstOrDefaultAsync(p => p.UserId == vr.PatientId, cancellationToken);
 
            var patientAddress = patientProfile?.User?.Address;

            visitRequestDtos.Add(new VisitRequestResponce
            {
                Id = vr.Id,
                PatientId = vr.PatientId,
                Description = vr.Description,
                DateTime = vr.DateTime,
                EndDateTime = vr.DateTimeEnd,
                Address = patientAddress,
                Status = vr.Status
            });
        }

        return visitRequestDtos;
    }


  
}



public class VisitRequestResponce
{
    public string PatientId { get; set; }
    public string Description { get; set; }
    public DateTime? DateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int Id { get; set; }
    public Address Address { get; set; }
    
    public VisitStatus Status { get; set; }
}

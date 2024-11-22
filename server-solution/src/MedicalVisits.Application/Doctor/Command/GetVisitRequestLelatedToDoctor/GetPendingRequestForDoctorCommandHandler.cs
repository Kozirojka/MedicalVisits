﻿using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Doctor.Command.GetVisitRequestLelatedToDoctor;

public class GetPendingRequestForDoctorCommandHandler : IRequestHandler<GetPendingRequestsForDoctorCommand, List<VisitRequestDtoNew>>
{
    
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;
    
    
    public GetPendingRequestForDoctorCommandHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<List<VisitRequestDtoNew>> Handle(GetPendingRequestsForDoctorCommand request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.VisitRequests
            .Where(u => request.Doctor.Id.Equals(u.DoctorId)) // Фільтруємо по лікарю
            .Where(u => u.Status == request.Doctor.Status)   // Фільтруємо по статусу
            .ToListAsync(cancellationToken);

        var visitRequestDtos = new List<VisitRequestDtoNew>();

        foreach (var vr in result)
        {
            var patientProfile = await _dbContext.PatientProfiles
                .Include(p => p.User) 
                .ThenInclude(u => u.Address) 
                .FirstOrDefaultAsync(p => p.UserId == vr.PatientId, cancellationToken);

            var patientAddress = patientProfile?.User?.Address;

            visitRequestDtos.Add(new VisitRequestDtoNew
            {
                Description = vr.Description,
                DateTime = vr.DateTime,
                EndDateTime = vr.DateTimeEnd,
                Address = patientAddress // Додаємо адресу в DTO
            });
        }

        return visitRequestDtos;
    }


  
}



public class VisitRequestDtoNew
{
    public string Description { get; set; }
    public DateTime? DateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    
    public Address Address { get; set; }
    
    public VisitStatus Status { get; set; }
}

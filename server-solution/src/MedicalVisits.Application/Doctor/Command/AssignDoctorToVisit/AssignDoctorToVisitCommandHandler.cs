using System.ComponentModel.DataAnnotations;
using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;

public class AssignDoctorToVisitCommandHandler : IRequestHandler<AssignDoctorToVisitCommand, AssignmentResult>
{
    
    public ApplicationDbContext _dbContext;
    public UserManager<ApplicationUser> _userManager;
    public Logger<AssignDoctorToVisitCommandHandler> _logger;
    
    public AssignDoctorToVisitCommandHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<AssignmentResult> Handle(AssignDoctorToVisitCommand request, CancellationToken cancellationToken)
    {
        var result = _dbContext.VisitRequests.FirstOrDefault(u => u.Id == request.VisitId);

        if (result == null)
        {
            Console.WriteLine("Visit with ID {VisitId} not found, request.VisitId");
            
            throw new NotFoundException($"Visit with ID {request.VisitId} not found");
        }

        var doctor = await _userManager.FindByIdAsync(request.DoctorId);
        if (doctor == null || !await _userManager.IsInRoleAsync(doctor, "Doctor"))
        {
            _logger.LogWarning("Invalid doctor ID {DoctorId} or user is not a doctor", request.DoctorId);
            throw new ValidationException("Invalid doctor ID or user is not a doctor"); 
            
        }
        result.Status = VisitStatus.DoctorAccepted;

        var visit = _dbContext.TimeSlots.SingleOrDefault(u => u.Id == request.SlotTimeId);
        
        visit?.SetVisitRequest(request.VisitId);
        
        
        await _dbContext.SaveChangesAsync();
        
        
        return new AssignmentResult
        {
            Success = true,
            Message = "Doctor successfully assigned to visit",
        };
        
    }
}
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}


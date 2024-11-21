using System.Security.Claims;
using MediatR;
using MedicalVisits.Application.Auth.Commands.RegisterPatient;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Patient.Command;

public class CreateVisitRequestCommandHandler : IRequestHandler<CreateVisitRequestCommand, CreateVisitRequestResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateVisitRequestCommandHandler(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<CreateVisitRequestResponse> Handle(
        CreateVisitRequestCommand request, 
        CancellationToken cancellationToken)
    {
        var patient = await _userManager.FindByIdAsync(request.PatientId);
        if (patient == null)
        {
            throw new ApplicationException("Patient not found");
        }

        var visitRequest = VisitRequest.Create(
            request.PatientId,
            request.DateTime,
            request.Description,
            request.Address
        );

        visitRequest.SetIsRegular(request.IsRegular);
        
        if (!string.IsNullOrEmpty(request.RequiredMedications))
        {
        }

        _context.VisitRequests.Add(visitRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateVisitRequestResponse
        {
            RequestId = visitRequest.Id,
            Message = "Appointment request created successfully"
        };
    }
}
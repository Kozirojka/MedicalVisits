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
        // Перевірка користувача
        var patient = await _userManager.FindByIdAsync(request.PatientId);
        if (patient == null)
        {
            throw new ApplicationException("Patient not found");
        }

        // Створення візиту
        var visitRequest = VisitRequest.Create(
            request.PatientId,
            request.DateTime,
            request.Description,
            request.Address
        );

        // Встановлення додаткових властивостей
        visitRequest.SetIsRegular(request.IsRegular);
        
        if (!string.IsNullOrEmpty(request.RequiredMedications))
        {
            visitRequest.AddRequiredMedication(request.RequiredMedications);
        }

        // Зберігаємо в базу
        _context.VisitRequests.Add(visitRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateVisitRequestResponse
        {
            RequestId = visitRequest.Id,
            Message = "Appointment request created successfully"
        };
    }
}
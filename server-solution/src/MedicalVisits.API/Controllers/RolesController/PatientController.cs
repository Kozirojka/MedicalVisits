using System.Security.Claims;
using System.Text.Json;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Patient.Command;
using MedicalVisits.Application.Patient.Command.CreateVisitRequest;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers.RolesController;

[Authorize(Roles = "Patient")]
[ApiController]
[Route("api/[controller]")]
public class PatientController : BaseController
{
    public PatientController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
    {
    }
    
    
    [HttpPost("request")]
    public async Task<IActionResult> CreateAppointment([FromBody] VisitRequestDto visitRequest)
    {
        var patientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (patientId == null)
            return Unauthorized();

        try
        {
            
            // Додаємо логування
            Console.WriteLine($"Отримано запит на створення візиту: {JsonSerializer.Serialize(visitRequest)}");

            var command = new CreateVisitRequestCommand(visitRequest, patientId);
            var result = await _Mediator.Send(command);

            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

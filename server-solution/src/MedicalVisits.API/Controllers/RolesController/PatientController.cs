using System.Security.Claims;
using System.Text.Json;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
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

    private readonly Logger<PatientController> _logger;
    public PatientController(IMediator mediator, UserManager<ApplicationUser> userManager, Logger<PatientController> logger) : base(mediator, userManager)
    {
        _logger = logger;
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
    
    [HttpGet("Users")]
    public async Task<IActionResult> GetAllUsers()
    {
        try 
        {
            _logger.LogInformation("Its function for get user");
            var command = new GetAllUsersQuery();
            var result = await _Mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Додайте логування
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}

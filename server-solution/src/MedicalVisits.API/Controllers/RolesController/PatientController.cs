using System.Security.Claims;
using System.Text.Json;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Application.Patient.Command;
using MedicalVisits.Application.Patient.Command.CreateVisitRequest;
using MedicalVisits.Application.Patient.Queries.GetAllVisitHistory;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Infrastructure.Services.UsersService;
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

    private readonly ILogger<PatientController> _logger;
    private readonly IUserService _userService;
    public PatientController(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<PatientController> logger, IUserService userService) : base(mediator, userManager)
    {
        _logger = logger;
        _userService = userService;
    }
    
    
    [HttpPost("request")]
    public async Task<IActionResult> CreateAppointment([FromBody] VisitRequestDto visitRequest)
    {
        var patientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (patientId == null)
            return Unauthorized();

        try
        {
            
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
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


    [HttpGet("visits/history")]
    public async Task<IActionResult> GetAllVisitHistory()
    {
        var userId = _userService.GetUserId(User);

        var query = new GetAllVisitHistoryQuery(userId);

        var commandResult = await _Mediator.Send(query);

        if (commandResult == null)
        {
            return BadRequest("There is no visits history");
        }


        return Ok(commandResult);
    }
    
    
}

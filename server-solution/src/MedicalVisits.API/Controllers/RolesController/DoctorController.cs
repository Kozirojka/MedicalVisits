using System.Security.Claims;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers.RolesController;


[ApiController]
[Authorize(Roles = "Doctor")]
[Route("api/[controller]")]
public class DoctorController : BaseController
{
    private readonly ILogger<DoctorController> _logger;
    
    public DoctorController(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<DoctorController> logger) : base(mediator, userManager)
    {
        _logger = logger;
    }

    [HttpGet("visits/pending")]
    public async Task<IActionResult> GetPendingVisits(
        [FromQuery] VisitStatus? status = null)
    {
        
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var dto = new DoctorRequestFilterDto 
        { 
            Id = doctorId,
            Status = VisitStatus.Approved
        };

        var getListOfAllVisitsToDoctor = new GetPendingRequestsForDoctorCommand(dto);
        var result = await _Mediator.Send(getListOfAllVisitsToDoctor);
        
        
        return Ok(result);
    }

    
    
    
    /*
     * todo: меотдл AssighnDoctor має проблеми з тим, що можна преедавати дані у body
      */
    [HttpPut("doctor/{visitId}")]
    public async Task<IActionResult> AssignDoctorToVisit(int visitId)
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new AssignDoctorToVisitCommand(visitId, doctorId);

        var result = await _Mediator.Send(command);

        
        return Ok(result);
    }
    
    

    //todo: Зроби безпечнішу функцію
    [HttpGet("visits/confirmed")]
    public async Task<IActionResult> GetConfirmedVisits()
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var command = new GetConfirmVisitRequestsCommand(doctorId);

        var result =  await  _Mediator.Send(command);
        
        return Ok(result);
    }
    
}


    
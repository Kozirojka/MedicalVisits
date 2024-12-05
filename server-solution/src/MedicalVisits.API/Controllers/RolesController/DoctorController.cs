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

[Authorize(Roles = "Doctor")]
public class DoctorController : BaseController
{
    public DoctorController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
    {
    }

    [HttpGet("visits/pending")]
    public async Task<IActionResult> GetTheListOfRelatedToDoctorPendingVisits(
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


    
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Doctor.Command.GetVisitRequestLelatedToDoctor;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;

[Authorize(Roles = "Doctor")]
public class DoctorController : BaseController
{
    public DoctorController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
    {
    }

    [HttpPost("GetVisitRequests")]
    public async Task<IActionResult> GetTheListOfRelatedToDoctorPendingVisits(DoctorRequestFilterDto dto)
    {
        var getListOfAllVisitsToDoctor = new GetPendingRequestsForDoctorCommand(dto);
        var result = await _Mediator.Send(getListOfAllVisitsToDoctor);

        return Ok(result);
        
    }
    
    
    
}


    
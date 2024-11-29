using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using MedicalVisits.Application.Doctor.Command.GetVisitRequestLelatedToDoctor;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
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

    [HttpPost("visits/pending")]
    public async Task<IActionResult> GetTheListOfRelatedToDoctorPendingVisits(DoctorRequestFilterDto dto)
    {
        var getListOfAllVisitsToDoctor = new GetPendingRequestsForDoctorCommand(dto);
        var result = await _Mediator.Send(getListOfAllVisitsToDoctor);

        return Ok(result);
        
    }

    
    
    
    /*
     * TODO: зробити так, щоб Id відписилалось не в url, так як це небезпечно
      */
    [HttpPut("{visitId}/doctor/{doctorId}")]
    public async Task<IActionResult> AssignDoctorToVisit(int visitId, string doctorId)
    {

        var command = new AssignDoctorToVisitCommand(visitId, doctorId);

        var result = _Mediator.Send(command);


        return Ok(result);
    }
    
    /*
     * TODO: створити фукнцію, яка буде  брати з бази даних Усі прийоми Лікаря,
     * буде відображати шляхи до цих прйомів які будуть оєднані  в одинх шлях
     * існує така можливість хешованого маршруту
     */

    [HttpGet("visits/confirmed")]
    public async Task<IActionResult> GetConfirmedVisits( RouteRequestDto requestDto)
    {
        var command = new GetConfirmVisitRequestsCommand(requestDto);

        var result =  _Mediator.Send(command);
        
        return Ok(result);
    }
    
}


    
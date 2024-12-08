using System.Security.Claims;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using MedicalVisits.Application.Doctor.Command.SetCustomeSchedule;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Application.Doctor.Queries.GetTimeSlotFor5Days;
using MedicalVisits.Infrastructure.Services;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Dtos.DoctorDto;
using MedicalVisits.Models.Dtos.Schedule.CreateScheduleDto;
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
    private readonly IDoctorScheduleService _scheduleService;
    private readonly ILogger<DoctorController> _logger;
    
    public DoctorController(IMediator mediator, UserManager<ApplicationUser> userManager, IDoctorScheduleService scheduleService, ILogger<DoctorController> logger) : base(mediator, userManager)
    {
        _scheduleService = scheduleService;
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


    

    
    [HttpPost("set-schedule")]
    public async Task<IActionResult> SetCustomSchedule(CreateScheduleRequest request)
    {
        _logger.LogInformation("We entered in this function");
        
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        request.DoctorId = doctorId;
        
        var command = new SetCustomeScheduleCommand(request);

        var result =  await _Mediator.Send(command);
        
        return Ok(result);
    }
    
    
    
    
    [HttpPost("assign-visit")]
    public async Task<IActionResult> AssignVisitToTimeSlot(AssignVisitRequest request)
    {
        try
        {
            await _scheduleService.AssignVisitToDoctorSlot(
                request.VisitRequestId, 
                request.TimeSlotId);

            return Ok(new { 
                message = "Visit successfully assigned to time slot",
                visitRequestId = request.VisitRequestId,
                timeSlotId = request.TimeSlotId
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while assigning the visit");
        }
    }
    
    [HttpGet("schedule/slots")]
    public async Task<IActionResult> GetTimeSlots([FromQuery] DateTime startDate)
    {
        try
        {
            Console.WriteLine("NOW WEEEE IN  CONTROLLLERRRw");
            var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (doctorId == null)
                return Unauthorized();

            var query = new GetDoctorTimeSlotsQuery
            {
                DoctorId = doctorId,
                StartDate = startDate.Date // Беремо тільки дату, без часу
            };

            var slots = await _Mediator.Send(query);
            
            return Ok(slots);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error fetching time slots");
        }
    }    
}


    
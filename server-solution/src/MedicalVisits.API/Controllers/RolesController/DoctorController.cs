using System.Security.Claims;
using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Doctor.Command.AddSlotToSchedule;
using MedicalVisits.Application.Doctor.Command.AssignDoctorToVisit;
using MedicalVisits.Application.Doctor.Command.CreateScheduleWithSlots;
using MedicalVisits.Application.Doctor.Queries.GetConfirmVisitRequests;
using MedicalVisits.Application.Doctor.Queries.GetMedicalCard;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Dtos.Schedule;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers.RolesController;


/// <summary>
/// Контролер для управління функціоналом лікаря в системі медичних візитів.
/// Забезпечує операції з візитами, розкладом та часовими слотами.
/// </summary>
/// <remarks>
/// Вимагає автентифікації та роль "Doctor" для доступу до всіх endpoint-ів
/// </remarks>
[ApiController]
[Authorize(Roles = "Doctor")]
[Route("api/[controller]")]
public class DoctorController : BaseController
{
    private readonly IUserService _userService;
    public DoctorController(IMediator mediator, UserManager<ApplicationUser> userManager, IUserService userService) : base(mediator, userManager)
    {
        _userService = userService;
    }

    /// <summary>
    /// Отримати візити які очікують розгляду лікаря та підтвердежння
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Метод потрібний для того, щоб призначити лікаря до запитут
    /// "Від цього моменту лікар буде головним за цей запит та обробляти його".
    /// в резульатті в базі даних заторкуються такі таблички.
    ///
    /// VisitRequest, TimeSlots, ScheduleWorkHours.
    /// </summary>
    /// <param name="visitId"></param>
    /// <param name="scheduleId"></param>
    /// <returns></returns>
    [HttpPut("doctor/{visitId}/{scheduleId}")]
    public async Task<IActionResult> AssignDoctorToVisit(int visitId, int scheduleId)
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new AssignDoctorToVisitCommand(visitId, doctorId, scheduleId);

        var result = await _Mediator.Send(command);

        
        return Ok(result);
    }
    
    
    // візит вважається підтвержнний якщо він був у AssignDoctorToVisit function
    //todo: пристосувати цю функцію, під наш розкад, потрібно добавити у повернення інформацію про TimeSlots
    //Timeslot якщо він підтверджений має інформацію про візит
    [HttpGet("visits/confirmed")]
    public async Task<IActionResult> GetConfirmedVisits()
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var command = new GetConfirmVisitRequestsCommand(doctorId);

        var result =  await  _Mediator.Send(command);
        
        return Ok(result);
    }


    /// <summary>
    /// 👨‍⚕️ Функція потрібна для того, щоб встановлювати графік лікаря
    /// І після створення графіку встановлювати часові рамки
    /// тоді коли лікар зможе прийняти пацієнта (TimeSlot)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("set/scheduleSlots")]
    public async Task<IActionResult> SetScheduleAndSlots(ScheduleRequest dto)
    {
        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateScheduleWithSlotsCommand(dto, doctorId);
        
        var result = await _Mediator.Send(command);
        
        return Ok(result);
    }


    
    /// <summary>
    /// Додавання слота до вже існуючих
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="sceduleId"></param>
    /// <returns></returns>
    [HttpPost("set/slot")]
    public async Task<IActionResult> AddSlotRequest(List<TimeSlotDto> dto, int sceduleId)
    {

        var doctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new AddSlotScheduleCommand(dto, sceduleId);
        
        var result = await _Mediator.Send(command);
        
        
        return Ok(result);
    }


    [HttpGet("medical-card")]
    public async Task<IActionResult> GetMedicalCard()
    {

        var userId = _userService.GetUserId(User);
        
        var query = new GetMedicalCardQuery();
        
        var medicalCard = await _Mediator.Send(query);
        
        return Ok(medicalCard);
    }
    
}

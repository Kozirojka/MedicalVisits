using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Command.AttachVisitToDoctor;
using MedicalVisits.Application.Admin.Command.CreateADoctor;
using MedicalVisits.Application.Admin.Queries.FindAppendingRequests;
using MedicalVisits.Application.Admin.Queries.GetAllDoctors;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Application.Admin.Queries.GetNearestDoctors;
using MedicalVisits.Application.Admin.Queries.GetPatient;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers.RolesController;


/// <summary>
/// Наразу у цьому конролері присутня можливість для функції
/// Взяти всіх користувачів
/// </summary>
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : BaseController
{
    private readonly ILogger<AdminController> _logger;
    
    public AdminController(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<AdminController> logger) 
        : base(mediator, userManager)
    {
        _logger = logger;
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

    [HttpGet("Patient")]
    public async Task<IActionResult> GetAllPatients()
    {
        try
        {
                var result = await _Mediator.Send(new GetPatientQuery());
        
                if (result == null)
                {
                    return BadRequest("some troulee");
                }
        
                return Ok(result);
                
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("Doctors")]
    public async Task<IActionResult> GetAllDoctors()
    {
        try
        {
            var result = await _Mediator.Send(new GetAllDoctorsQuery());

            if (result == null)
            {
                return BadRequest("some troulee");
            }

            return Ok(result);
                
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("VisitsRequest")]
    public async Task<IActionResult> GetPendingVisitRequests()
    {
        try
        {
            var command = new FindAppendingRequestsQuery();
            var result = await _Mediator.Send(command);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("Attach-VisitRequest")]
    public async Task<IActionResult> AttachVisitResultToDoctor(InformationAboutVisitAndDoctorDTo dto)
    {
        try
        {
            var command = new AttachVisitToDoctorCommand()
            {
                DoctorId = dto.DoctorId,
                VisitRequestId = dto.VisitId
            };

            ResultOfAttach result = await _Mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Failed to assign the doctor.");
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in AttachVisitResultToDoctor method: {e.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost("Doctor")]
    public async Task<IActionResult> CreateDoctorAccount(RegisterDoctorDto dto)
    {
        try
        {
            var command = new CreateADoctorCommand(dto);
            var result = await _Mediator.Send(command); 

            if (!result.Succeeded)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating doctor account: {ex.Message}");

            return StatusCode(500, new { error = $"An unexpected error occurred: {ex.Message}" });
        }
    }
    
    //потрібно поправити, так, щоб передавалась, id пацієнта, і уже на основі цього id запиту буде визначатись адреса 
    [HttpGet("Nearest-Doctor/{VisitRequestId}")]  
    public async Task<IActionResult> GetListOfNearestDoctors([FromRoute(Name = "VisitRequestId")] int VisitRequestId) 
    {
        if (VisitRequestId <= 0)  
        {
            return BadRequest("Ідентифікатор запиту повинен бути більше 0.");
        }

        try 
        {
            var query = new GetNearestDoctorsQuery(VisitRequestId);
            var result = await _Mediator.Send(query); 

            if (result == null)
            {
                return NotFound($"Не знайдено результатів для запиту з ID: {VisitRequestId}");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Сталася помилка при обробці запиту.");
        }
    }
}

public class InformationAboutVisitAndDoctorDTo
{
    public string DoctorId { get; set; }
    public int VisitId { get; set; }
}

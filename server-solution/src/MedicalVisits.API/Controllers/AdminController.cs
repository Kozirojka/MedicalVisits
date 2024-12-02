using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Command.AttachVisitRequestToDoctor;
using MedicalVisits.Application.Admin.Command.CreateADoctor;
using MedicalVisits.Application.Admin.Queries.FindAppendingRequest;
using MedicalVisits.Application.Admin.Queries.GetAllDoctors;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Application.Admin.Queries.GetNearestDoctors;
using MedicalVisits.Application.Admin.Queries.GetPatient;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : BaseController
{
    public AdminController(IMediator mediator, UserManager<ApplicationUser> userManager) 
        : base(mediator, userManager)
    {
    }

    
    [HttpGet("Users")]
    public async Task<IActionResult> GetAllUsers()
    {
        
        try 
        {
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
            var result = await _Mediator.Send(new GetDoctorsQuery());

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
            var command = new FindAppendingRequestQuery();
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

            resultOfAttach result = await _Mediator.Send(command);

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
    [HttpPost("Nearest-Doctor")]
    public async Task<IActionResult> GetListOfNearestDoctors(AddressDto dto)
    {

        
        var query = new GetListOfNearestDoctorQuery(dto);

        var resultOfquery = await _Mediator.Send(query);

        if (resultOfquery == null)
        {
            return BadRequest("у тебе великі проблеми є у цьому житті");
        }
        
        return Ok(resultOfquery);
    }
}

public class InformationAboutVisitAndDoctorDTo
{
    public string DoctorId { get; set; }
    public int VisitId { get; set; }
}

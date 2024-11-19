using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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

    
    [HttpGet("users")]
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
    
    
}

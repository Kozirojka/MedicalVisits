using MediatR;
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
    public IActionResult GetAllUsers()
    {
        
        
        
        return Ok();
    }
}

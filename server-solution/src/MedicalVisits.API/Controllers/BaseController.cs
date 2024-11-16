using MediatR;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _Mediator;
    protected readonly UserManager<ApplicationUser> _UserManager;

    protected BaseController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
}
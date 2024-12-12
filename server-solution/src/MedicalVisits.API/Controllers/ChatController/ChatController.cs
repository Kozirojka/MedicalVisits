using MediatR;
using MedicalVisits.API.Controllers.Base;
using MedicalVisits.Application.Admin.Queries.GetAllUser;
using MedicalVisits.Application.Chat.CreatePrivateChat;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models.Dtos.AuthDto;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalVisits.API.Controllers.ChatController;

[ApiController]
[Authorize(Roles = "Doctor, Admin, Patient")]
[Route("api/[controller]")]
public class ChatController : BaseController
{
    private readonly IUserService _userService;
    
    public ChatController(IMediator mediator, UserManager<ApplicationUser> userManager, IUserService userService) : base(mediator, userManager)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var command = new GetAllUsersQuery();

        var usersList = await _Mediator.Send(command);


        if (usersList == null)
        {
            return NotFound();
        }
        

        return Ok(usersList);
    }

    [HttpPost("private-chat")]
    public async Task<IActionResult> CreatePrivateChat(UserDto chatPrivateRequest)
    {
        string? userId = _userService.GetUserId(User);
        if (userId == null)
        {
            return NotFound("main user not found");
        }

        var command = new CreatePrivateChatCommand(chatPrivateRequest, userId);
        var result = await _Mediator.Send(command);


        if (result == null)
            return BadRequest($"Result after CreatePrivateChatCommandHandler is dalse {result}");
        
        
        return Ok(result);
    }
}

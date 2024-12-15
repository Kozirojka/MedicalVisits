using System.Security.Claims;
using MedicalVisits.Infrastructure.Services.Interfaces;

namespace MedicalVisits.Infrastructure.Services.UsersService;

public class UserService : IUserService
{
    public string? GetUserId(ClaimsPrincipal? user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
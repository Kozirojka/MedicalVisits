using System.Security.Claims;
using MedicalVisits.Infrastructure.Services.Interfaces;

namespace MedicalVisits.Infrastructure.Services.UserService;

public class UserService : IUserService
{
    public string? GetUserId(ClaimsPrincipal? user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
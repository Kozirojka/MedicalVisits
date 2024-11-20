namespace MedicalVisits.Models.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}


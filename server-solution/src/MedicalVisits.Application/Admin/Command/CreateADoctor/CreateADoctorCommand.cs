using MediatR;
using MedicalVisits.Application.Auth.Commands.CreatePatient;
using MedicalVisits.Models;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Command.CreateADoctor;

public class CreateADoctorCommand : IRequest<AuthResult>
{
    public CreateADoctorCommand(RegisterDoctorDto driverRequest)
    {
        DriverRequest = driverRequest;
    }

    public RegisterDoctorDto DriverRequest { get; set; }
    
}

public class RegisterDoctorDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public Address Address { get; set; }
    
    public string Specialization { get; set; }
    public string LicenseNumber { get; set; }
}

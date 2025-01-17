using FastEndpoints;
using MediatR;
using MedicalVisits.Application.Admin.Command.CreateADoctor;
using MedicalVisits.Application.Auth.Commands.CreatePatient;

namespace MedicalVisits.API.Feature.Admin.CreateUser;

public class CreateDoctorAccountEndpoint(IMediator _mediator) : Endpoint<RegisterDoctorDto, AuthResult>
{
    public override void Configure()
    {
        Post("/api/v2/doctor"); 
        Roles("Admin");      
    }

    public override async Task HandleAsync(RegisterDoctorDto dto, CancellationToken ct)
    {
        var command = new CreateADoctorCommand(dto);
        var result = await _mediator.Send(command, ct);

        if (!result.Succeeded)
        {
            await SendNotFoundAsync(ct); 
            return;
        }

        await SendOkAsync(result, ct);
    }
}
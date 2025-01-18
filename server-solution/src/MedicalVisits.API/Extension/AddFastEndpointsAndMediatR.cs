using FastEndpoints;
using FastEndpoints.Swagger;
using MedicalVisits.Application.Auth.Commands.CreatePatient;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;

namespace MedicalVisits.API.Extension;

public static class FastEndpointsAndMediatRExtensions
{
    public static IServiceCollection AddFastEndpointsAndMediatR(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.SwaggerDocument();
        // MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GenerateAccessTokenCommand).Assembly));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreatePatientCommand).Assembly));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<GetPendingRequestsForDoctorCommand>());

        return services;
    }
}
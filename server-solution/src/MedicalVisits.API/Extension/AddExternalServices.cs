using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Infrastructure.Services.MessagesService;
using MedicalVisits.Infrastructure.Services.UsersService;
using Microsoft.AspNetCore.SignalR;

namespace MedicalVisits.API.Extension;

public static class ExternalServicesRegistrationExtensions
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration config)
    {
        
        //для роботи з монгодб
        services.AddSingleton<IMessagesService, MessagesService>();
        
        //GeocodingService та RouteService для роботи з гугл картами
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<IRouteService, RouteService>();
        
        
        //щоб визначати id та роль користувача через jwt
        services.AddScoped<IUserService, UserService>();

        services.AddHttpClient();
        
        //сігналр + кастомний провайдер
        services.AddSignalR();
        services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

        return services;
    }
}
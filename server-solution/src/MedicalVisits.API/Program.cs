using System.Text;
using MediatR;
using MedicalVisits.Application.Auth.Commands.CreatePatient;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services;
using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Infrastructure.Services.UsersService;
using MedicalVisits.Infrastructure.SignalR.Hubs;
using MedicalVisits.Models.Configurations;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GenerateAccessTokenCommand).Assembly));

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreatePatientCommand).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPendingRequestsForDoctorCommand>());
builder.Services.AddScoped<IGeocodingService, GeocodingService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>(); 


builder.Services.Configure<GoogleMapsServiceSettings>(
    builder.Configuration.GetSection("GoogleMaps"));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    // Налаштовуємо фільтри для різних просторів імен
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)   
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)  
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) 
    .WriteTo.Console()  // Все ще логуємо в консоль для розробки
    .WriteTo.Seq("http://localhost:5341", restrictedToMinimumLevel: LogEventLevel.Information)
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Host.UseSerilog();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {   
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:SecretKey"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000", "http://localhost:5268")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});






builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical Visits API", Version = "v1" });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
} );


var app = builder.Build();


Log.Information("Application is running!");
Log.Information("http://localhost:5268");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});
app.UseCors("AllowSpecificOrigin");

	
app.MapHub<ChatHub>("/ChatHub");

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();

app.Run();

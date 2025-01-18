using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using MedicalVisits.API;
using MedicalVisits.API.Extension;
using MedicalVisits.Application.Auth.Commands.CreatePatient;
using MedicalVisits.Application.Auth.Commands.GenerateAccessToken;
using MedicalVisits.Application.Doctor.Queries.GetPendingVisitRequests;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Infrastructure.Services.MessagesService;
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

builder.Services.Configure<MessageDatabaseSettings>(builder.Configuration.GetSection("MessageDatabaseSettings"));

builder.Services.Configure<GoogleMapsServiceSettings>(
    builder.Configuration.GetSection("GoogleMaps"));

builder.Host.UseSerilogLogging();
builder.Services.AddFastEndpointsAndMediatR();
builder.Services.AddMongoDbServiceExtension(builder.Configuration);
builder.Services.AddSwaggerWithJwtSupport();
builder.Services.AddExternalServices(builder.Configuration);


builder.Services.AddLogging();



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
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }

                var path = context.HttpContext.Request.Path;
                if (path.StartsWithSegments("/ChatHub"))
                {
                    context.Token ??= context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                }

                return Task.CompletedTask;
            }
        };

    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000", "https://localhost:7145")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


builder.Services.AddControllers();

var app = builder.Build();

Log.Information("Application is running!");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});
app.UseCors("AllowSpecificOrigin");

app.UseFastEndpoints();
app.UseSwaggerGen();



app.MapHub<ChatHub>("/ChatHub");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

using System.Security.Claims;
using MediatR;
using MedicalVisits.API.Controllers;
using MedicalVisits.Application.Patient.Command;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test;

[TestFixture]
public class Tests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<IMediator> _mediatorMock;
    private ApplicationDbContext _dbContext;
    private PatientController _controller;
    
    
    [SetUp]
    public void Setup()
    {
        // Налаштування UserManager
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        
        // Налаштування InMemory DB
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ApplicationDbContext(options);
        
        // Налаштування Mediator
        _mediatorMock = new Mock<IMediator>();

        // Налаштування контролера
        _controller = new PatientController(_mediatorMock.Object, _userManagerMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "test-patient-id")
                    }))
                }
            }
        };
    }
    
    
    [TearDown]
    public void TearDown()
    {
        // Утилізуємо _dbContext після виконання тесту
        _dbContext.Dispose();
    }

    //
    // [Test]
    // public async Task Test1()
    // {
    //     // Arrange
    //     var visitRequestDto = new VisitRequestDto
    //     {
    //         DateTime = DateTime.UtcNow,
    //         DateTimeEnd = DateTime.UtcNow.AddHours(1),
    //         Description = "Test visit",
    //         Address = "Test address",
    //         IsRegular = true,
    //         HasMedicine = false,
    //         RequiredMedications = "Paracetamol"
    //     };
    //     
    //     var command = new CreateVisitRequestCommand(visitRequestDto, "test-patient-id");
    //
    //     _mediatorMock
    //         .Setup(m => m.Send(It.IsAny<CreateVisitRequestCommand>(), It.IsAny<CancellationToken>()))
    //         .Callback<CreateVisitRequestCommand, CancellationToken>((cmd, _) =>
    //         {
    //             // Емулюємо обробку команди, додаючи візит у базу даних
    //             var visitRequest = VisitRequest.Create(
    //                 cmd.PatientId, cmd.DateTime, cmd.Description, cmd.Address);
    //             visitRequest.SetIsRegular(cmd.IsRegular);
    //             _dbContext.VisitRequests.Add(visitRequest);
    //             _dbContext.SaveChanges();
    //         })
    //         .ReturnsAsync(new CreateVisitRequestResponse
    //         {
    //             RequestId = 1,
    //             Message = "Appointment request created successfully"
    //         });
    //
    //     // Act
    //     var result = await _controller.CreateAppointment(visitRequestDto) as OkObjectResult;
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.AreEqual(200, result.StatusCode);
    //
    //     var visitRequestInDb = _dbContext.VisitRequests.FirstOrDefault();
    //     Assert.NotNull(visitRequestInDb);
    //     Assert.AreEqual("test-patient-id", visitRequestInDb.PatientId);
    //     Assert.AreEqual("Test visit", visitRequestInDb.Description);
    //     Assert.AreEqual("Test address", visitRequestInDb.Address);
    //     Assert.IsTrue(visitRequestInDb.IsRegular);
    // }
    
    
}

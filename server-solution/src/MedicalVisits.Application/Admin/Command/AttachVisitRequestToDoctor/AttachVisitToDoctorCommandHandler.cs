using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Command.AttachVisitRequestToDoctor;

public class AttachVisitToDoctorCommandHandler : IRequestHandler<AttachVisitToDoctorCommand, resultOfAttach>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context; // Додайте контекст

    public AttachVisitToDoctorCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _mediator = mediator;
        _context = context;
    }

    public async Task<resultOfAttach> Handle(AttachVisitToDoctorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"VisitId: {request.VisitRequestId} ----------------------------");
            Console.WriteLine($"DoctorId: {request.DoctorId} ---------------------------------");
            
            int idTemp = request.VisitRequestId;
            
           var visit = await _context.VisitRequests.FindAsync(idTemp);
            //Console.WriteLine($"--------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! {visit.Status}");
            
            // Логування значень перед оновленням статусу

            // if (visit == null)
            // {
            //     return new resultOfAttach
            //     {
            //         Result = false,
            //         Message = "VisitRequest not found"
            //     };
            // }
            
            // Логування перед збереженням змін
            Console.WriteLine("Saving changes...");

           // await _context.SaveChangesAsync(cancellationToken);

            return new resultOfAttach
            {
                Result = true,
                Message = "Doctor assigned successfully"
            };
        }
        catch (Exception ex)
        {
            // Логування помилки
            Console.WriteLine($"Error in Handle method: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return new resultOfAttach
            {
                Result = false,
                Message = "An error occurred while assigning the doctor"
            };
        }
    }
}

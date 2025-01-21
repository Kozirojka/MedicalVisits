using MediatR;
using MedicalVisits.Infrastructure.Persistence;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace MedicalVisits.Application.Admin.Command.AttachVisitToDoctor;

public class AttachVisitToDoctorCommandHandler : IRequestHandler<AttachVisitToDoctorCommand, ResultOfAttach>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context; 

    public AttachVisitToDoctorCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _mediator = mediator;
        _context = context;
    }

    public async Task<ResultOfAttach> Handle(AttachVisitToDoctorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"VisitId: {request.VisitRequestId} ----------------------------");
            Console.WriteLine($"DoctorId: {request.DoctorId} ---------------------------------"); 
            
            int idTemp = request.VisitRequestId;
            
           var visit = await _context.VisitRequests.FindAsync(idTemp);
           
            //Console.WriteLine($"--------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! {visit.Status}");
            
            if (visit == null)
            {
                return new ResultOfAttach
                {
                    Result = false,
                    Message = "VisitRequest not found"
                };
            }
            
            visit.AssignDoctor(request.DoctorId);
            visit.Status = VisitStatus.Approved;
            
            
           
            Console.WriteLine("Saving changes...");

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultOfAttach
            {
                Result = true,
                Message = "Doctor assigned successfully"
            };
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Error in Handle method: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return new ResultOfAttach
            {
                Result = false,
                Message = "An error occurred while assigning the doctor"
            };
        }
    }
}

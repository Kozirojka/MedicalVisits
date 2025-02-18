using MediatR;
using MedicalVisits.Models.Entities.ScheduleV2;

namespace MedicalVisits.Application.Doctor.Command.Schedule.SetInterval;

public class SetIntervalCommand(
    DateTime StartInterval,
    DateTime EndInterval,
    int? DoctorScheduleId, 
    int? VisitRequestId,
    string doctorId) : IRequest<bool>
{
    public DateTime StartInterval { get; } = StartInterval;
    public DateTime EndInterval { get; } = EndInterval;
    public int? DoctorScheduleId { get; } = DoctorScheduleId;
    public int? VisitRequestId { get; } = VisitRequestId;

    public readonly string DoctorId = doctorId;

}
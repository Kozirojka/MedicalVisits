using MediatR;
using MedicalVisits.Models.Entities.ScheduleV2;

namespace MedicalVisits.Application.Doctor.Queries.GetIntervals;

public class GetIntervalsQuery(string _doctorId) : IRequest<List<DoctorIntervals>>
{
    public readonly string doctorId = _doctorId;
};
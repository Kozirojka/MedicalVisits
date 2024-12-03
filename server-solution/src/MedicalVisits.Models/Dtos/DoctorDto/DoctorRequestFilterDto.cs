using MedicalVisits.Models.Enums;

namespace MedicalVisits.Models.Dtos;

public class DoctorRequestFilterDto
{
    public string Id { get; set; } // ID лікаря

    public VisitStatus? Status { get; set; } // Nullable enum для фільтрації за статусом
}

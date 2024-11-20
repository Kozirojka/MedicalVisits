namespace MedicalVisits.Models.Dtos;

public class VisitRequestDto
{
    public int? Id { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime DateTimeEnd { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public bool IsRegular { get; set; }
    public bool HasMedicine { get; set; }
    public string RequiredMedications { get; set; }
}

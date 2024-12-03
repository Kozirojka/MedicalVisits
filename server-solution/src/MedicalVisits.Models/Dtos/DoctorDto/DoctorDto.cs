namespace MedicalVisits.Application.Admin.Queries.GetAllDoctors;

public class DoctorDto
{
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Specialization { get; set; }
        
        public string LicenseNumber { get; set; }
}
using MediatR;
using MedicalVisits.Models.Entities;

namespace MedicalVisits.Application.Admin.Queries.GetNearestDoctors;

public class GetListOfNearestDoctorQuery : IRequest<List<GetListOfNearestDoctorQueryHandler.DoctorProfileWithDistance>>
{
    public GetListOfNearestDoctorQuery(AddressDto address)
    {
        Address = address;
    }

    
    public AddressDto Address { get; private set; }
}


public class AddressDto{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Bulding { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
}
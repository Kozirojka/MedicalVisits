namespace MedicalVisits.Models;

public class Address
{
    public string City { get; }
    public string Street { get; }
    public string Building { get; }
    public string Apartment { get; }
    public string Region { get; } // Область
    public string Country { get; }

    private Address() { } // Для EF Core

    public string GetStreet() => Street;
    public Address(string city, string street, string building, string region, string country, string apartment)
    {
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required");
        if (string.IsNullOrWhiteSpace(building)) throw new ArgumentException("Building is required");
        if (string.IsNullOrWhiteSpace(region)) throw new ArgumentException("Region is required");
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");

        City = city;
        Street = street;
        Building = building;
        Apartment = apartment;
        Region = region;
        Country = country;
    }

    public override string ToString()
    {
        var location = $"{Street}, {Building},{City}, {Region}, {Country}";

        return location;
    }

    protected bool Equals(Address other)
    {
        return City == other.City &&
               Street == other.Street &&
               Building == other.Building &&
               Apartment == other.Apartment &&
               Region == other.Region &&
               Country == other.Country;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is Address other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(City, Street, Building, Apartment, Region, Country);
    }
}
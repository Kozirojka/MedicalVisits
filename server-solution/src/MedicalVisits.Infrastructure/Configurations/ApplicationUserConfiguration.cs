using MedicalVisits.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.City).HasMaxLength(100).IsRequired();
            address.Property(a => a.Street).HasMaxLength(200).IsRequired();
            address.Property(a => a.Building).HasMaxLength(50).IsRequired();
            address.Property(a => a.Apartment).HasMaxLength(50);
            address.Property(a => a.Region).HasMaxLength(100).IsRequired();
            address.Property(a => a.Country).HasMaxLength(100).IsRequired();
        });
        
      
    }
}
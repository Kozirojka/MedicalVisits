using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class SchedulesConfigurationV2: IEntityTypeConfiguration<DoctorSchedules>
{
    public void Configure(EntityTypeBuilder<DoctorSchedules> builder)
    {
        builder.HasKey(ds => ds.Id);

        builder.HasOne(ds => ds.Doctor)
            .WithMany()
            .HasForeignKey(ds => ds.DoctorId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany<DoctorIntervals>()
            .WithOne(di => di.DoctorSchedules)
            .HasForeignKey(di => di.DoctorScheduleId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
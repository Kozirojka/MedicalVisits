using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class DoctorIntervalsConfigurationV2 : IEntityTypeConfiguration<DoctorIntervals>
{
    public void Configure(EntityTypeBuilder<DoctorIntervals> builder)
    {
        builder.HasKey(di => di.Id);

        builder.HasOne(di => di.DoctorSchedules)
            .WithMany()
            .HasForeignKey(di => di.DoctorScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(di => di.Doctor)
            .WithMany()
            .HasForeignKey(di => di.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
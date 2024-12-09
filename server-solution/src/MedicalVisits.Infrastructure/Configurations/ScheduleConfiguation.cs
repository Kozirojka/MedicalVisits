using MedicalVisits.Models.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalVisits.Infrastructure.Configurations;

public class ScheduleConfiguation : IEntityTypeConfiguration<ScheduleWorkPlan>
{
    public void Configure(EntityTypeBuilder<ScheduleWorkPlan> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(u => u.TimeSlots).WithOne(x => x.WorkPlan).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(plan => plan.User).WithMany().HasForeignKey(x => x.UserId);
                
        
    }
}

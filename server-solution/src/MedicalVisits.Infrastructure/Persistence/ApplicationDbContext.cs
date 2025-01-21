using MedicalVisits.Infrastructure.Configurations;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.ChatEntities;
using MedicalVisits.Models.Entities.ScheduleV2;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<VisitRequest> VisitRequests { get; set; }
    public DbSet<PatientProfile> PatientProfiles { get; set; }
    public DbSet<DoctorProfile> DoctorProfiles { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatParticipants> ChatParticipants { get; set; }  
    
    
    public DbSet<DoctorIntervals> DoctorIntervals { get; set; }
    public DbSet<DoctorSchedules> DoctorSchedules { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.ApplyConfiguration(new ChatParticipantsConfiguration());
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new VisitRequestConfiguration());
        builder.ApplyConfiguration(new DoctorProfileConfiguration());
        builder.ApplyConfiguration(new PatientProfileConfiguration());
        builder.ApplyConfiguration(new DoctorIntervalsConfigurationV2());
        builder.ApplyConfiguration(new SchedulesConfigurationV2());
        
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "ee81f2e5-ca09-4653-81f9-1b88134634fd"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Doctor",
                NormalizedName = "DOCTOR",
                ConcurrencyStamp = "96c3cf11-235f-408f-b93d-6138df94cb17"
            },
            new IdentityRole
            {
                Id = "3",
                Name = "Patient",
                NormalizedName = "PATIENT",
                ConcurrencyStamp = "ed5b1acf-d8bf-46d6-b42e-5e51cbaf9ab4"
            },
            new IdentityRole
            {
                Id = "4",
                Name = "Nurse",
                NormalizedName = "NURSE",
                ConcurrencyStamp = "905b99f5-8079-48c6-bef9-2af1ded0cba4"
            }
        );
        
    }
    
}

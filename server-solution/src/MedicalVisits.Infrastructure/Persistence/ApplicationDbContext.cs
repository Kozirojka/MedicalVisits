﻿using MedicalVisits.Infrastructure.Configurations;
using MedicalVisits.Models.Entities;
using MedicalVisits.Models.Entities.Schedule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MedicalVisits.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<VisitRequest> VisitRequests { get; set; }
    public DbSet<DoctorWorkSchedule> DoctorWorkSchedules { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<PatientProfile> PatientProfiles { get; set; }
    public DbSet<DoctorProfile> DoctorProfiles { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DoctorProfile>()
            .HasOne(dp => dp.User)
            .WithOne()
            .HasForeignKey<DoctorProfile>(dp => dp.UserId);

        builder.Entity<DoctorWorkSchedule>()
            .HasOne(ws => ws.Doctor)
            .WithMany(d => d.WorkSchedules)
            .HasForeignKey(ws => ws.DoctorId)
            .HasPrincipalKey(d => d.UserId) 
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TimeSlot>()
            .HasOne(ts => ts.Schedule)
            .WithMany(ws => ws.TimeSlots)
            .HasForeignKey(ts => ts.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TimeSlot>()
            .HasOne(ts => ts.AssignedVisit)
            .WithOne(v => v.TimeSlot)
            .HasForeignKey<VisitRequest>(v => v.TimeSlotId);

        builder.Entity<VisitRequest>()
            .HasOne(vr => vr.Patient)
            .WithMany()
            .HasForeignKey(vr => vr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VisitRequest>()
            .HasOne(vr => vr.Doctor)
            .WithMany()
            .HasForeignKey(vr => vr.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // builder.ApplyConfiguration(new DoctorWorkScheduleConfiguration());
        // builder.ApplyConfiguration(new TimeSlotConfiguration());
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new VisitRequestConfiguration());
        builder.ApplyConfiguration(new DoctorProfileConfiguration());
        builder.ApplyConfiguration(new PatientProfileConfiguration());
        
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Doctor",
                NormalizedName = "DOCTOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Id = "3",
                Name = "Patient",
                NormalizedName = "PATIENT",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Id = "4",
                Name = "Nurse",
                NormalizedName = "NURSE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
        
        
        var hasher = new PasswordHasher<ApplicationUser>();
        var adminId = Guid.NewGuid().ToString();

        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminId,
                UserName = "admin@medicalvisits.com",
                NormalizedUserName = "ADMIN@MEDICALVISITS.COM",
                Email = "admin@medicalvisits.com",
                NormalizedEmail = "ADMIN@MEDICALVISITS.COM",
                EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"), 
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "User",
                LockoutEnabled = false
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = adminId,
                RoleId = "1" 
            }
        );
    }
    
}

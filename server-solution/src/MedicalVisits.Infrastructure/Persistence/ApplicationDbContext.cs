using MedicalVisits.Models.Entities;
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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        // Придушуємо попередження про pending changes
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)
                .Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

     
        
        
        
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

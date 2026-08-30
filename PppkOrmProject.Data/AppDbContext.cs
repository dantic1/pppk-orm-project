using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Data;

public class AppDbContext : DbContext
{
    private string CONNECTION_STRING = "Host=localhost;Port=5432;Database=hospital;Username=doctor;Password=password";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder
        //     .UseNpgsql(CONNECTION_STRING)
        //     .EnableSensitiveDataLogging()
        //     .LogTo(Console.WriteLine);


        optionsBuilder
            .UseNpgsql(CONNECTION_STRING)
            .UseLazyLoadingProxies();
    }
    
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Examination> Examinations =>  Set<Examination>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Disease> Diseases => Set<Disease>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Prescription>  Prescriptions => Set<Prescription>();
    public DbSet<MedicalHistory>  MedicalHistories => Set<MedicalHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Patient>()
            .HasIndex(a => a.Oib)
            .IsUnique();
        
        modelBuilder.Entity<Disease>()
            .HasIndex(a => a.Name)
            .IsUnique();
        
        modelBuilder.Entity<Medication>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Patient>()
            .Property(p => p.Gender)
            .HasConversion<string>()
            .HasMaxLength(10);


        modelBuilder.Entity<Examination>()
            .Property(p => p.ExaminationType)
            .HasConversion<string>()
            .HasMaxLength(10);
        
        modelBuilder.Entity<Examination>()
            .Property(e => e.ScheduledAt)
            .HasColumnType("timestamp without time zone");

    }
}
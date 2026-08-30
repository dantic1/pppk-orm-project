using System.ComponentModel.DataAnnotations;
using PppkOrmProject.Data.Enums;

namespace PppkOrmProject.Data.Models;

public class Examination
{
    public int Id { get; set; }
    
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    
    [Required]
    public ExaminationType ExaminationType { get; set; }
    
    [Required]
    public DateTime ScheduledAt { get; set; }
    
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
}
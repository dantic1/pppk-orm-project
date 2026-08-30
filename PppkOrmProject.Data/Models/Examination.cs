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
    
    [MaxLength(500)]
    public string? Notes {get; set;}
    
    public virtual Patient Patient { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
}
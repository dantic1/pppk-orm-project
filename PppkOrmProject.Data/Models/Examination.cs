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
    
    public required Patient Patient { get; set; }
    public required Doctor Doctor { get; set; }
}
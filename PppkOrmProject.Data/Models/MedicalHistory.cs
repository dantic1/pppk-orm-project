using System.ComponentModel.DataAnnotations;

namespace PppkOrmProject.Data.Models;

public class MedicalHistory
{
    public int Id { get; set; }
    
    // FKs
    public int PatientId { get; set; }
    public int DiseaseId { get; set; }
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
    
    public Patient Patient { get; set; } = null!;
    public Disease Disease { get; set; } = null!;
}
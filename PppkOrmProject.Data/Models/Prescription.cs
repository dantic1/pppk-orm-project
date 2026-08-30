using System.ComponentModel.DataAnnotations;

namespace PppkOrmProject.Data.Models;

public class Prescription
{
    public int Id { get; set; }
    
    public int PatientId { get; set; }
    public int MedicationId { get; set; }
    public int? MedicalHistoryId  { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Dosage {get; set;} = String.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Frequency {get; set;} = String.Empty;
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
    
    public Patient Patient { get; set; } = null!;
    public Medication Medication { get; set; } = null!;
    public MedicalHistory? MedicalHistory { get; set; }
}
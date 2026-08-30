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
    
    public virtual Patient Patient { get; set; } = null!;
    public virtual Medication Medication { get; set; } = null!;
    public virtual MedicalHistory? MedicalHistory { get; set; }
}
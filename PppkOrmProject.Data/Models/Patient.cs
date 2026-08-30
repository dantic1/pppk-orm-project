using System.ComponentModel.DataAnnotations;
using PppkOrmProject.Data.Enums;

namespace PppkOrmProject.Data.Models;

public class Patient
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Required]
    [MaxLength(11)]
    public string Oib { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(150)]
    public string ResidenceAddress { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(150)]
    public string PermanentAddress { get; set; } = String.Empty;
    
    public virtual List<MedicalHistory> MedicalHistories { get; set; } = new();
    public List<Prescription> Prescriptions { get; set; } = new();
    public List<Examination> Examinations { get; set; } = new();
    
}
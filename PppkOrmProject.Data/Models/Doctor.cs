using System.ComponentModel.DataAnnotations;

namespace PppkOrmProject.Data.Models;

public class Doctor
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Specialization { get; set; } = String.Empty;
}
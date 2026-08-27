using System.ComponentModel.DataAnnotations;

namespace PppkOrmProject.Data.Models;

public class Medication
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    
    [MaxLength(100)]
    public string? Manufacturer { get; set; }
}
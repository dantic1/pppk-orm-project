using System.ComponentModel.DataAnnotations;

namespace PppkOrmProject.Data.Models;

public class Disease
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
}
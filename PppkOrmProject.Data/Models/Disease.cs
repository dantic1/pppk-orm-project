using System.ComponentModel.DataAnnotations;
using PppkOrmProject.Data.TestOrm.Attributes;

namespace PppkOrmProject.Data.Models;

[Table("Diseases")] 
public class Disease
{
    [Column("Id",  IsPrimaryKey=true)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("Name")] 
    public string Name { get; set; } = String.Empty;
    
    [MaxLength(500)]
    [Column("Description")]
    public string? Description { get; set; }
}
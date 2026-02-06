using System.ComponentModel.DataAnnotations;

namespace EcomHub.Domain.Entities;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

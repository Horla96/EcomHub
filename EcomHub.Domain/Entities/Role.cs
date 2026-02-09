using Microsoft.AspNetCore.Identity;

namespace EcomHub.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public string? Description { get; set; }
    public string? CreatedBy { get; set; } = "System";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

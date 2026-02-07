using EcomHub.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EcomHub.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    //BaseEntity
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

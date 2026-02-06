namespace EcomHub.Domain.Entities;

public class ShoppingCart : BaseEntity
{
    public Guid UserId { get; set; }
    public bool IsActive { get; set; } = true;
}

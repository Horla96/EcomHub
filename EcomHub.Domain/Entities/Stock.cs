namespace EcomHub.Domain.Entities;

public class Stock : BaseEntity
{
    public Guid ProductId { get; set; }
    public int TotalQuantity { get; set; }
    public int RemainingQuantity { get; set; }
    public bool IsOutOfStock { get; set; } = false;
}

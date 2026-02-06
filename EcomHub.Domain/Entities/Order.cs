using EcomHub.Domain.Enums;

namespace EcomHub.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}

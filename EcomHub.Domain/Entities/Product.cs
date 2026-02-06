using EcomHub.Domain.Enums;

namespace EcomHub.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    public ProductStatus Status { get; set; } 
}

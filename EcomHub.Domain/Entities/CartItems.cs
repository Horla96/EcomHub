namespace EcomHub.Domain.Entities;

public class CartItems : BaseEntity
{
    public Guid ShoppingCartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }


}

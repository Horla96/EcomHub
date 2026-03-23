namespace EcomHub.Application.DTOs.Requests;

public class ProductFilterRequestDto
{
    public Guid? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? CreatedAfter { get; set; }
}

namespace EcomHub.Application.DTOs.Requests;

public class ProductQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;


    public string? SortBy { get; set; } = "CreatedAt"; 
    public string? SortOrder { get; set; } = "desc";   
}

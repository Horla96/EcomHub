using EcomHub.Domain.Entities;

namespace EcomHub.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<List<Product>> SearchProductAsync(string keyword);
    Task<List<Product>> FilterProductsAsync(Guid? categoryId, decimal? minPrice, decimal? maxPrice, DateTime? createdAfter);
    Task<List<Product>> GetAllProductAsync();
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize, string? sortBy, string? sortOrder);

}

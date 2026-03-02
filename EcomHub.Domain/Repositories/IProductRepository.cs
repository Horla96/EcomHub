using EcomHub.Domain.Entities;

namespace EcomHub.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductAsync();
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
}

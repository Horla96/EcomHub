using EcomHub.Domain.Entities;
using EcomHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcomHub.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Product> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _context.Products
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}

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

    public async Task<List<Product>> SearchProductAsync(string keyword)
    {
        return await _context.Products
        .Where(p => p.Name.ToLower().Contains(keyword.ToLower()))
        .ToListAsync();
    }

    public async Task<List<Product>> FilterProductsAsync(Guid? categoryId, decimal? minPrice, decimal? maxPrice, DateTime? createdAfter)
    {
        var query = _context.Products.AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice);

        if (createdAfter.HasValue)
            query = query.Where(p => p.CreatedAt >= createdAfter);

        return await query.ToListAsync();
    }

    public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize, string? sortBy, string? sortOrder)
    {
        var query = _context.Products.AsQueryable();

        query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
        {
            ("price", "asc") => query.OrderBy(p => p.Price),
            ("price", "desc") => query.OrderByDescending(p => p.Price),

            ("name", "asc") => query.OrderBy(p => p.Name),
            ("name", "desc") => query.OrderByDescending(p => p.Name),

            ("createdat", "asc") => query.OrderBy(p => p.CreatedAt),
            ("createdat", "desc") => query.OrderByDescending(p => p.CreatedAt),

            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var skip = (pageNumber - 1) * pageSize;

        query = query
            .Skip(skip)
            .Take(pageSize);

        return await query.ToListAsync();
    }
}

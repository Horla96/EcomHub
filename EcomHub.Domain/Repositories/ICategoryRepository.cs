using EcomHub.Domain.Entities;

namespace EcomHub.Domain.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category> CreateAsync(Category category);
    Task UpdateAsync(Category category);
}

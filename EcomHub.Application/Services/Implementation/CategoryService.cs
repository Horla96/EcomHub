using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;
using EcomHub.Application.Services.Interfaces;
using EcomHub.Domain.Entities;
using EcomHub.Domain.Repositories;

namespace EcomHub.Application.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryRequestDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            CreatedBy = "Admin"
        };

        var created = await _categoryRepository.CreateAsync(category);

        return new CategoryResponseDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            IsActive = created.IsActive
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        category.IsDeleted = true;
        category.UpdatedAt = DateTime.Now;

        await _categoryRepository.UpdateAsync(category);
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            IsActive = c.IsActive
        }).ToList();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null) return null;

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryRequestDto request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        category.Name = request.Name;
        category.Description = request.Description;
        category.UpdatedAt = DateTime.Now;

        await _categoryRepository.UpdateAsync(category);
    }
}

using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;

namespace EcomHub.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(Guid id);
    Task<CategoryResponseDto> CreateAsync(CreateCategoryRequestDto request);
    Task UpdateAsync(Guid id, UpdateCategoryRequestDto request);
    Task DeleteAsync(Guid id);
}

using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;

namespace EcomHub.Application.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> CreateAsync(CreateProductRequestDto request);
    Task<List<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto?> UpdateAsync(Guid id, UpdateProductRequestDto request);
    Task<bool> DeleteAsync(Guid id);
    Task<List<ProductSearchResponseDto>> SearchProductAsync(string keyword);
    Task<List<ProductResponseDto>> FilterProductsAsync(ProductFilterRequestDto request);
    Task<List<ProductResponseDto>> GetProductsAsync(ProductQueryDto request);
}

using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;
using EcomHub.Application.Services.Interfaces;
using EcomHub.Domain.Entities;
using EcomHub.Domain.Enums;
using EcomHub.Domain.Repositories;

namespace EcomHub.Application.Services.Implementation;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductRequestDto request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId,
            Status = (ProductStatus)request.StockQuantity

        };

        await _productRepository.CreateProductAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            //Name = product.Name,
            //Description = product.Description,
            //Price = product.Price,
            //StockQuantity = product.StockQuantity,
            //Status = product.Status
        };
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllProductAsync();

        return products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            //Name = product.Name,
            //Description = product.Description,
            //Price = product.Price,
            //StockQuantity = product.StockQuantity,
            //Status = product.Status
        }).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
            return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            //Name = product.Name,
            //Description = product.Description,
            //Price = product.Price,
            //StockQuantity = product.StockQuantity,
            //Status = product.Status
        };
    }

    public async Task<ProductResponseDto?> UpdateAsync(Guid id, UpdateProductRequestDto request)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
            return null;

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.Status = (ProductStatus)request.StockQuantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateProductAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            //Name = product.Name,
            //Description = product.Description,
            //Price = product.Price,
            //StockQuantity = product.StockQuantity,
            //Status = product.Status
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
            return false;

        await _productRepository.DeleteProductAsync(product);

        return true;
    }

    public async Task<List<ProductSearchResponseDto>> SearchProductAsync(string keyword)
    {
        var products = await _productRepository.SearchProductAsync(keyword);

        return products.Select(product => new ProductSearchResponseDto
        {
            //Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Status = product.Status
        }).ToList();
    }

    public async Task<List<ProductResponseDto>> FilterProductsAsync(ProductFilterRequestDto request)
    {
        var products = await _productRepository.FilterProductsAsync(
            request.CategoryId,
            request.MinPrice,
            request.MaxPrice,
            request.CreatedAfter
        );

        return products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Status = product.Status

        }).ToList();
    }

    public async Task<List<ProductResponseDto>> GetProductsAsync(ProductQueryDto request)
    {
        var products = await _productRepository.GetProductsAsync(
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortOrder
        );

        return products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Status = product.Status

        }).ToList();
    }
}

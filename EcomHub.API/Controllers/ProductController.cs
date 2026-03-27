using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcomHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> Create(CreateProductRequestDto request)
    {
        var result = await _productService.CreateAsync(request);

        return Ok(result);
    }

    [HttpGet("get-all-products")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("get-product/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (result == null)
            return NotFound("Product not found");

        return Ok(result);
    }

    [HttpPut("update-product/{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequestDto request)
    {
        var result = await _productService.UpdateAsync(id, request);

        if (result == null)
            return NotFound("Product not found");

        return Ok(result);
    }

    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Product not found");

        return Ok("Product deleted successfully");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProduct([FromQuery] string keyword)
    {
        var result = await _productService.SearchProductAsync(keyword);

        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterProducts(ProductFilterRequestDto request)
    {
        var result = await _productService.FilterProductsAsync(request);

        return Ok(result);
    }

    [HttpPost("products")]
    public async Task<IActionResult> GetProducts(ProductQueryDto request)
    {
        var result = await _productService.GetProductsAsync(request);

        return Ok(result);
    }
}

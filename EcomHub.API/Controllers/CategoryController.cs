using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcomHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("create-category")]
    public async Task<IActionResult> Create(CreateCategoryRequestDto request)
    {
        var result = await _categoryService.CreateAsync(request);

        return Ok(result);
    }

    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("update-category/{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequestDto request)
    {
        await _categoryService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id); 

        return NoContent();
    }
}

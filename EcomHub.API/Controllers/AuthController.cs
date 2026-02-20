using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.Services.Interfaces;
using EcomHub.Domain.DTOs.Requests;
using EcomHub.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcomHub.API.Controllers;

public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register-user")]
    [ProducesDefaultResponseType(typeof(RegisterUserResponseDto))]
    public async Task<IActionResult> RegisterUser(RegisterUserResquestDto request)
    {
        var result = await _userService.RegisterAsync(request);

        return Ok(result);
    }

    [HttpPost("login")]
    [ProducesDefaultResponseType(typeof(LoginResponseDto))]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var result = await _userService.LoginAsync(request);

        return Ok(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto request)
    {
        var response = await _userService.ForgotPasswordAsync(request);

        if (response == null)
        {
            return BadRequest(new
            {
                Message = "User not found"
            });
        }

        return Ok(response);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto request)
    {
        var response = await _userService.ChangePasswordAsync(request);

        if (!response.IsSuccessful)
            return BadRequest(response);

        return Ok(response);
    }
}

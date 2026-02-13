using EcomHub.Application.Services.Interfaces;
using EcomHub.Domain.DTOs.Requests;
using EcomHub.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcomHub.API.Controllers
{
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
    }
}

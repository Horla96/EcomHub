using EcomHub.Application.DTOs.Requests;
using EcomHub.Domain.DTOs.Requests;
using EcomHub.Domain.DTOs.Responses;

namespace EcomHub.Application.Services.Interfaces;

public interface IUserService
{
    Task<RegisterUserResponseDto> RegisterAsync(RegisterUserResquestDto request);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}

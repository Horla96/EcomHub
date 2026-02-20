using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;
using EcomHub.Domain.DTOs.Requests;
using EcomHub.Domain.DTOs.Responses;

namespace EcomHub.Application.Services.Interfaces;

public interface IUserService
{
    Task<RegisterUserResponseDto> RegisterAsync(RegisterUserResquestDto request);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request);
    Task<ChangePasswordResponseDto> ChangePasswordAsync(ChangePasswordRequestDto request);
}

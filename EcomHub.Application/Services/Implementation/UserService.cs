using EcomHub.Application.DTOs.Requests;
using EcomHub.Application.DTOs.Responses;
using EcomHub.Application.Interfaces;
using EcomHub.Application.Services.Interfaces;
using EcomHub.Domain.DTOs.Requests;
using EcomHub.Domain.DTOs.Responses;
using EcomHub.Domain.Entities;
using EcomHub.Domain.Repositories;
using EcomHub.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace EcomHub.Application.Services.Implementation;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly AppDbContext _context;
    private readonly IOtpService _otpService;
    public UserService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, AppDbContext context, IOtpService otpService)
	{
		_userRepository = userRepository;	
		_userManager = userManager;
		_signInManager = signInManager;
		_jwtService = jwtService;
		_context = context;
		_otpService = otpService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new LoginResponseDto
            {
                Message = "Invalid email or password",
                Status = false,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            return new LoginResponseDto
            {
                Message = "Invalid email or password",
                Status = false,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        var tokenResult = _jwtService.GenerateAccessToken(user, roles);


        return new LoginResponseDto
        {
            Message = "Login successful",
            Status = true,
            StatusCode = HttpStatusCode.OK,
            Token = tokenResult.Token,
            Expiration = tokenResult.Expiration
        };
    }

    public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserResquestDto request)
	{
		var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
		if (existingUser != null)
			return new RegisterUserResponseDto
			{
				Message = "User already exists",
				Status = false,
				StatusCode = HttpStatusCode.NotFound
			};

		var roleExists = await _userRepository.GetRoleByName(request.Role.ToString());
		if(roleExists is null)
		{
			return new RegisterUserResponseDto
			{
				Message = "Role does not exists",
				Status = false,
				StatusCode = HttpStatusCode.NotFound
			};
		}

		var user = new User
		{
			Id = Guid.NewGuid(),
			Email = request.Email,
			FirstName = request.FirstName,
			LastName = request.LastName,
			FullName = request.FirstName + " " + request.LastName,
			UserName = request.UserName,
			Role = request.Role,
            IsActive = true,
			IsDeleted = false,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};
		
		var result = await _userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
		{
			return new RegisterUserResponseDto
			{
				Message = "Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)),
				Status = false,
				StatusCode = HttpStatusCode.InternalServerError
			};
        }
        else
		{
			var userRoleResult = await _userManager.AddToRoleAsync(user, roleExists.Name);
			if (!userRoleResult.Succeeded)
			{
				return new RegisterUserResponseDto
				{
					Message = "Failed to assign role to user: " + string.Join(", ", userRoleResult.Errors.Select(e => e.Description)),
					Status = false,
					StatusCode = HttpStatusCode.InternalServerError
				};
            }
		}

		return new RegisterUserResponseDto
		{
			Message = "User Registered Successfully",
			Status = true,
			StatusCode = HttpStatusCode.OK
		};
    }

    public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new Exception("User does not exist");

        var code = _otpService.GenerateOtp(); 

		var newOtp = new Otp
		{
			Id = Guid.NewGuid(),
			Code = code,
			UserId = user.Id,
			Purpose = "PasswordReset",
			IsUsed = false,
			IsActive = true,
			ExpiryTime = DateTime.UtcNow.AddMinutes(5)
		};

        await _context.Otps.AddAsync(newOtp);
        await _context.SaveChangesAsync();

        return new ForgotPasswordResponseDto
        {
            Message = "OTP generated successfully",
            Otp = code
        };
    }

    public async Task<ChangePasswordResponseDto> ChangePasswordAsync(ChangePasswordRequestDto request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            return new ChangePasswordResponseDto
            {
                Message = "User not found",
                IsSuccessful = false
            };
        }
        var otp = await _context.Otps
            .Where(x => x.UserId == user.Id
                    && x.Code == request.OtpCode
                    && x.Purpose == "PasswordReset"
                    && x.IsActive
                    && !x.IsUsed)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (otp == null)
        {
            return new ChangePasswordResponseDto
            {
                Message = "Invalid OTP",
                IsSuccessful = false
            };
        }

        if (otp.ExpiryTime < DateTime.UtcNow)
        {
            return new ChangePasswordResponseDto
            {
                Message = "OTP has expired",
                IsSuccessful = false
            };
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

        if (!result.Succeeded)
        {
            return new ChangePasswordResponseDto
            {
                Message = "Password reset failed",
                IsSuccessful = false
            };
        }

        otp.IsUsed = true;
        otp.IsActive = false;

        _context.Otps.Update(otp);
        await _context.SaveChangesAsync();

        return new ChangePasswordResponseDto
        {
            Message = "Password Changed Successfully",
            IsSuccessful = true
        };
    }
}


//Register
//Login
//Forgot Password
//Get Profile
//Update Profile
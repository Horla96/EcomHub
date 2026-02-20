namespace EcomHub.Application.Services.Interfaces;

public interface IOtpService
{
    string GenerateOtp(int length = 6);
}

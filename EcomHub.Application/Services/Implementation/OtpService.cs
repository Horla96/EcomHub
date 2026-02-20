using EcomHub.Application.Services.Interfaces;
using System.Security.Cryptography;

namespace EcomHub.Application.Services.Implementation;

public class OtpService : IOtpService
{
    public string GenerateOtp(int length = 6)
    {
        var min = (int)Math.Pow(10, length - 1);
        var max = (int)Math.Pow(10, length) - 1;

        return RandomNumberGenerator
            .GetInt32(min, max)
            .ToString();
    }
}

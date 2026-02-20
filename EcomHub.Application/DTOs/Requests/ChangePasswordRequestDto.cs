namespace EcomHub.Application.DTOs.Requests;

public class ChangePasswordRequestDto
{
    public string UserId { get; set; }
    public string OtpCode { get; set; }
    public string NewPassword { get; set; }
}

using EcomHub.Domain.Enums;

namespace EcomHub.Application.DTOs.Requests;

public class RegisterUserResquestDto
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public string Password { get; set; }
}

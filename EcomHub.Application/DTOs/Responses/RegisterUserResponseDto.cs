using System.Net;

namespace EcomHub.Domain.DTOs.Responses;

public class RegisterUserResponseDto
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}

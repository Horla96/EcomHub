using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcomHub.Domain.DTOs.Responses;

public class LoginResponseDto
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Token { get; set; }
    public DateTime? Expiration { get; set; }
}

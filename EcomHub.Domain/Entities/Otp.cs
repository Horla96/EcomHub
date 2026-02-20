namespace EcomHub.Domain.Entities;

public class Otp
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid UserId { get; set; }
    public string Purpose { get; set; }

    public bool IsUsed { get; set; }
    public bool IsActive { get; set; }

    public DateTime ExpiryTime { get; set; }
}

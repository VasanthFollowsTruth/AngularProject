namespace Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}

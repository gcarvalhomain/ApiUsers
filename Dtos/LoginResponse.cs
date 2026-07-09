namespace ApiUsers.Dtos;

public class LoginResponse
{
    // Token JWT que o cliente deve enviar no header Authorization: Bearer {token}.
    public string? Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

namespace ApiUsers.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
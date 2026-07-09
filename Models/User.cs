namespace ApiUsers.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    // Armazena apenas o hash gerado pelo PasswordHasher; nunca persistir senha em texto puro.
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    // O valor precisa bater com as roles usadas nas policies de autorizacao.
    public string Role { get; set; } = "User";
}

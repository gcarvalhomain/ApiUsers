namespace ApiUsers.Dtos;

// DTO publico do usuario. Nao inclui PasswordHash para evitar vazamento de credenciais.
public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

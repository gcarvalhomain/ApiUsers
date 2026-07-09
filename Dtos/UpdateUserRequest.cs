namespace ApiUsers.Dtos;

// Entrada de atualizacao completa dos dados basicos do usuario.
public class UpdateUserRequest
{
    public string? Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public int Age { get; set; } 
}

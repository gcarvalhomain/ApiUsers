namespace ApiUsers.Responses;

// Formato padrao para erros simples retornados pelos endpoints.
public class ErrorResponse
{
    public string Message { get; set; } = String.Empty;
}

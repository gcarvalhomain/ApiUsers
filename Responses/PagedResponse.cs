using ApiUsers.Dtos;

namespace ApiUsers.Responses;

// Envelope reutilizavel para endpoints que retornam listas paginadas.
public class PagedResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = [];
}

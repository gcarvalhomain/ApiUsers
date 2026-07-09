using ApiUsers.Responses;
using ApiUsers.Dtos;
using ApiUsers.Service;

namespace ApiUsers.Endpoints;

public static class UserEndpoints
{

    public static void MapUserEndpoints(this WebApplication app)
{
    // Listagem publica com limites defensivos para evitar consultas grandes demais.
    app.MapGet("/api/users", async (
        int? page, int? pageSize, string? search, string? sortyBy, string? sortedDirection, UserService userService) =>
    {
        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? 10;

        if (currentPage <= 0)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message = "Page must be greater than zero."
            });
        }

        if (currentPageSize <= 0)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message = "Page size must be greater than zero."
            });
        }

        if (currentPageSize > 50)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message = "Page size cannot be greater than 50."
            });
        }

        var users = await userService.GetAllAsync( 
            currentPage, currentPageSize, search,  sortyBy, sortedDirection);

        return Results.Ok(users);
    });

    app.MapGet("/api/users/{id}", async (int id, UserService userService) =>
    {
        var user =  await userService.GetByIdAsync(id);

        if (user is null)
        {
            return Results.NotFound(new ErrorResponse
            {
                Message = "User not found."
            });
        }

        return Results.Ok(user);
    });

    app.MapPost("/api/users", async (CreateUserRequest request, UserService userService) =>
    {
        var validationError = ValidateUser(request.Name, request.Email, request.Age);
        if (validationError is not null )
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message = validationError
            });
        }
        var emailExists = await userService.EmailExistsAsync(request.Email);
        if (emailExists)
        {
            return Results.Conflict(new ErrorResponse
            {
                Message = "Email already exists."
            });
        }
        var user = await  userService.CreateAsync(request);

        return Results.Created($"/api/users/{user.Id}", user);
    })
    // Criacao direta de usuario exige login; cadastro publico com senha fica em /api/auth/register.
    .RequireAuthorization();

    app.MapPut("/api/users/{id}", async (int id, UpdateUserRequest request, UserService userService) =>
    {
        
        var validationError =  ValidateUser(request.Name, request.Email, request.Age);

        if (validationError is not null)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message =  validationError
            });
        }
        var emailExistsForAnotherUser = await userService.EmailExistsForAnotherUserAsync(request.Email!, id);
        if (emailExistsForAnotherUser)
        {
            return Results.BadRequest(new ErrorResponse
            {
                Message = "Email already exists."
            });
        }
        var updated = await userService.UpdateAsync(id, request);

        if (!updated)
        {
            return Results.NotFound(new ErrorResponse
            {
                Message = "User not found."
            });
        }
        
        var user = await  userService.GetByIdAsync(id);

        return Results.Ok(user);
    })
    .RequireAuthorization();

    // Remocao usa a policy AdminOnly configurada em Program.cs.
    app.MapDelete("/api/users/{id}",  async (int id, UserService userService) =>
    {
        var deleted = await userService.DeleteAsync(id);

        if (!deleted)
        {
            return Results.NotFound(new ErrorResponse
            {
                Message = "User not found."
            });
        }

        return Results.NoContent();
    })
    .RequireAuthorization("AdminOnly");
}

    private static string? ValidateUser(string? name, string? email, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Name is required.";
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return "Email is required.";
        }

        if (age <= 0)
        {
            return "Age must be greater than zero.";
        }

        return null;
    }
}

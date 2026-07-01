using ApiUsers.Dtos;
using ApiUsers.Responses;
using ApiUsers.Service;
using System.Security.Claims;

namespace ApiUsers.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (RegisterUserRequest request, AuthService authService) =>
        {
            var validationError = ValidateRegister(request);

            if (validationError is not null)
            {
                return Results.BadRequest(new ErrorResponse
                {
                    Message = validationError
                });
            }

            var user = await authService.RegisterAsync(request);

            if (user is null)
            {
                return Results.Conflict(new ErrorResponse
                {
                    Message = "Email already exists."
                });
            }

            return Results.Created($"/api/users/{user.Id}", user);
        });

        app.MapPost("/api/auth/login", async (LoginRequest request, AuthService authService) =>
        {
            var validationError = ValidateLogin(request);

            if (validationError is not null)
            {
                return Results.BadRequest(new ErrorResponse
                {
                    Message = validationError
                });
            }

            var loginResponse = await authService.LoginAsync(request);

            if (loginResponse is null)
            {
                return Results.BadRequest(new ErrorResponse
                {
                    Message = "Invalid email or password."
                });
            }
            return Results.Ok(loginResponse);
        });
        
        app.MapGet("/api/auth/me", (ClaimsPrincipal user) =>
            {
                var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var name = user.FindFirstValue(ClaimTypes.Name);
                var email = user.FindFirstValue(ClaimTypes.Email);

                return Results.Ok(new
                {
                    Id = id,
                    Name = name,
                    Email = email
                });
            })
            .RequireAuthorization();
    }

    private static string? ValidateRegister(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return "Name is required.";
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return "Email is required.";
        }

        if (request.Age <= 0)
        {
            return "Age must be greater than zero.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return "Password is required.";
        }

        if (request.Password.Length < 6)
        {
            return "Password must have at least 6 characters.";
        }

        return null;
    }

    private static string? ValidateLogin(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return "Email is required.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return "Password is required.";
        }

        return null;
    }
}
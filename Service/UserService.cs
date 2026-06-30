using ApiUsers.Data;
using ApiUsers.Dtos;
using ApiUsers.Responses;
using ApiUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUsers.Service;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<UserResponse>> GetAllAsync(
        int page, int pageSize, string? search, string? sortBy,  string? sortDirection)
    {
        var query = _context.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.Trim();
            
            query = query.Where(user => user.Name!.Contains(searchTerm)
            || user.Email!.Contains(searchTerm));
        }
        var isDescending = sortDirection?.ToLower() == "desc";

        query = sortBy?.ToLower() switch
        {
            "name" => isDescending
                ? query.OrderByDescending(user => user.Name)
                : query.OrderBy(user => user.Name),

            "email" => isDescending
                ? query.OrderByDescending(user => user.Email)
                : query.OrderBy(user => user.Email),

            "age" => isDescending
                ? query.OrderByDescending(user => user.Age)
                : query.OrderBy(user => user.Age),

            "createdat" => isDescending
                ? query.OrderByDescending(user => user.CreatedAt)
                : query.OrderBy(user => user.CreatedAt),

            _ => isDescending
                ? query.OrderByDescending(user => user.Id)
                : query.OrderBy(user => user.Id)
        };
        
        var totalItems = await query.CountAsync();
            
            var user = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name!,
                Email = user.Email!,
                Age = user.Age,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            })
            .ToListAsync();
            return new PagedResponse<UserResponse>()
            {
                Page = page,
                PageSize =  pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = user
            };
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user  = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return null;
        }

        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name!,
            Email = user.Email!,
            Age = user.Age,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
           
            
        };
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Age = user.Age,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return false;
        }

        user.Name = request.Name!;
        user.Email = request.Email!;
        user.Age = request.Age;
        user.CreatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users
            .AnyAsync(user => user.Email == email);
    }

    public async Task<bool> EmailExistsForAnotherUserAsync(string email, int userId)
    {
        return await _context.Users
            .AnyAsync(user => user.Email == email && user.Id == userId);
    }
}
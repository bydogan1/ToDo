using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        return await _context.Todos
            .Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Category = t.Category,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<TodoDto?> GetByIdAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return null;

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Category = todo.Category,
            IsCompleted = todo.IsCompleted,
            DueDate = todo.DueDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<TodoDto> CreateAsync(CreateTodoDto createDto)
    {
        var todo = new Todo
        {
            Title = createDto.Title,
            Description = createDto.Description,
            Category = createDto.Category,
            IsCompleted = false,
            DueDate = createDto.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Category = todo.Category,
            IsCompleted = todo.IsCompleted,
            DueDate = todo.DueDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<TodoDto?> UpdateAsync(int id, UpdateTodoDto updateDto)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return null;

        if (updateDto.Title != null)
            todo.Title = updateDto.Title;
        
        if (updateDto.Description != null)
            todo.Description = updateDto.Description;
        
        if (updateDto.Category != null)
            todo.Category = updateDto.Category;
        
        if (updateDto.IsCompleted.HasValue)
            todo.IsCompleted = updateDto.IsCompleted.Value;

        if (updateDto.DueDate.HasValue)
            todo.DueDate = updateDto.DueDate;

        todo.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Category = todo.Category,
            IsCompleted = todo.IsCompleted,
            DueDate = todo.DueDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }
}


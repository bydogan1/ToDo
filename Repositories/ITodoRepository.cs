using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoDto>> GetAllAsync();
    Task<TodoDto?> GetByIdAsync(int id);
    Task<TodoDto> CreateAsync(CreateTodoDto createDto);
    Task<TodoDto?> UpdateAsync(int id, UpdateTodoDto updateDto);
    Task<bool> DeleteAsync(int id);
}


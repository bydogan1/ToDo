using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Repositories;
using MyApi.Validators;
using MyApi.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=todos.db"));

// Repository
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();

// Database migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

// OpenAPI configuration
app.MapOpenApi();

// Root endpoint - static files middleware zaten index.html'i serve ediyor
app.MapGet("/", () => Results.Redirect("/index.html"));

// API Endpoints
app.MapGet("/api/todos", async (ITodoRepository repository, bool? completed = null, string? search = null, string? category = null) =>
{
    var todos = await repository.GetAllAsync();
    
    // Filtreleme
    if (completed.HasValue)
    {
        todos = todos.Where(t => t.IsCompleted == completed.Value);
    }
    
    // Kategori filtreleme
    if (!string.IsNullOrWhiteSpace(category))
    {
        todos = todos.Where(t => t.Category == category);
    }
    
    // Arama
    if (!string.IsNullOrWhiteSpace(search))
    {
        var searchLower = search.ToLower();
        todos = todos.Where(t => 
            t.Title.ToLower().Contains(searchLower) ||
            (!string.IsNullOrEmpty(t.Description) && t.Description.ToLower().Contains(searchLower))
        );
    }
    
    return Results.Ok(todos);
})
.WithName("GetAllTodos")
.WithTags("Todos")
.Produces<IEnumerable<TodoDto>>(StatusCodes.Status200OK);

app.MapGet("/api/todos/{id}", async (int id, ITodoRepository repository) =>
{
    var todo = await repository.GetByIdAsync(id);
    if (todo == null)
        return Results.NotFound(new { message = "Todo bulunamadı." });
    
    return Results.Ok(todo);
})
.WithName("GetTodoById")
.WithTags("Todos")
.Produces<TodoDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/todos", async (CreateTodoDto createDto, ITodoRepository repository) =>
{
    var validationResult = CreateTodoDtoValidator.Validate(createDto);
    if (validationResult != ValidationResult.Success)
    {
        return Results.BadRequest(new { message = validationResult?.ErrorMessage });
    }

    var todo = await repository.CreateAsync(createDto);
    return Results.Created($"/api/todos/{todo.Id}", todo);
})
.WithName("CreateTodo")
.WithTags("Todos")
.Produces<TodoDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

app.MapPut("/api/todos/{id}", async (int id, UpdateTodoDto? updateDto, ITodoRepository repository) =>
{
    if (updateDto == null)
        return Results.BadRequest(new { message = "Güncelleme verisi gönderilmedi." });
    
    var todo = await repository.UpdateAsync(id, updateDto);
    if (todo == null)
        return Results.NotFound(new { message = "Todo bulunamadı." });
    
    return Results.Ok(todo);
})
.WithName("UpdateTodo")
.WithTags("Todos")
.Produces<TodoDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapDelete("/api/todos/{id}", async (int id, ITodoRepository repository) =>
{
    var deleted = await repository.DeleteAsync(id);
    if (!deleted)
        return Results.NotFound(new { message = "Todo bulunamadı." });
    
    return Results.NoContent();
})
.WithName("DeleteTodo")
.WithTags("Todos")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.Run();

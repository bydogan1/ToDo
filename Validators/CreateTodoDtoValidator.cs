using System.ComponentModel.DataAnnotations;
using MyApi.DTOs;

namespace MyApi.Validators;

public class CreateTodoDtoValidator
{
    public static ValidationResult Validate(CreateTodoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return new ValidationResult("Title alanı zorunludur.");
        }

        if (dto.Title.Length > 200)
        {
            return new ValidationResult("Title en fazla 200 karakter olabilir.");
        }

        if (dto.Description?.Length > 1000)
        {
            return new ValidationResult("Description en fazla 1000 karakter olabilir.");
        }

        return ValidationResult.Success!;
    }
}


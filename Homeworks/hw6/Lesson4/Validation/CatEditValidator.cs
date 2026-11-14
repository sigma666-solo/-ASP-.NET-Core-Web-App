using FluentValidation;
using Lesson4.Models;

namespace Lesson4.Validation;

/// <summary>
/// Правила валидации формы редактирования котика.
/// </summary>
public class CatEditValidator : AbstractValidator<Cat>
{
    public const int MaxNameLength = 300;
    public const int MaxColorLength = 100;
    public const int MaxDescriptionLength = 2000;

    public CatEditValidator()
    {
        RuleFor(cat => cat.Name)
            .NotEmpty()
            .WithMessage("Имя котика обязательно")
            .MaximumLength(MaxNameLength)
            .WithMessage($"Имя котика не может быть свыше {MaxNameLength} символов");

        RuleFor(cat => cat.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Возраст не может быть меньше 0");

        RuleFor(cat => cat.Color)
            .NotEmpty()
            .WithMessage("Цвет котика обязателен")
            .MaximumLength(MaxColorLength)
            .WithMessage($"Цвет котика не может быть свыше {MaxColorLength} символов");

        RuleFor(cat => cat.Description)
            .NotEmpty()
            .WithMessage("Описание обязательно")
            .MaximumLength(MaxDescriptionLength)
            .WithMessage($"Описание не может быть свыше {MaxDescriptionLength} символов");

        RuleFor(cat => cat.UrlPhoto)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Ссылка на фотографию обязательна")
            .Must(BeHttpOrHttpsUrl)
            .WithMessage("Укажите корректную ссылку на фотографию (http или https)");
    }

    private static bool BeHttpOrHttpsUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}

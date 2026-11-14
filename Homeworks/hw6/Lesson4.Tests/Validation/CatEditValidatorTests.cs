using FluentValidation.Results;
using Lesson4.Models;
using Lesson4.Validation;

namespace Lesson4.Tests.Validation;

public class CatEditValidatorTests
{
    private readonly CatEditValidator _validator = new();

    [Fact]
    public void ValidCat_HasNoValidationErrors()
    {
        var result = _validator.Validate(CreateValidCat());

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyName_HasRequiredError(string? name)
    {
        var cat = CreateValidCat();
        cat.Name = name!;

        var result = _validator.Validate(cat);

        AssertHasError(result, nameof(Cat.Name), "Имя котика обязательно");
    }

    [Fact]
    public void NameLongerThanMaxLength_HasError()
    {
        var cat = CreateValidCat();
        cat.Name = new string('a', CatEditValidator.MaxNameLength + 1);

        var result = _validator.Validate(cat);

        AssertHasError(
            result,
            nameof(Cat.Name),
            $"Имя котика не может быть свыше {CatEditValidator.MaxNameLength} символов");
    }

    [Fact]
    public void NameAtMaxLength_IsValid()
    {
        var cat = CreateValidCat();
        cat.Name = new string('a', CatEditValidator.MaxNameLength);

        var result = _validator.Validate(cat);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void NegativeAge_HasError()
    {
        var cat = CreateValidCat();
        cat.Age = -1;

        var result = _validator.Validate(cat);

        AssertHasError(result, nameof(Cat.Age), "Возраст не может быть меньше 0");
    }

    [Fact]
    public void ZeroAge_IsValid()
    {
        var cat = CreateValidCat();
        cat.Age = 0;

        var result = _validator.Validate(cat);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyColor_HasRequiredError(string? color)
    {
        var cat = CreateValidCat();
        cat.Color = color!;

        var result = _validator.Validate(cat);

        AssertHasError(result, nameof(Cat.Color), "Цвет котика обязателен");
    }

    [Fact]
    public void ColorLongerThanMaxLength_HasError()
    {
        var cat = CreateValidCat();
        cat.Color = new string('c', CatEditValidator.MaxColorLength + 1);

        var result = _validator.Validate(cat);

        AssertHasError(
            result,
            nameof(Cat.Color),
            $"Цвет котика не может быть свыше {CatEditValidator.MaxColorLength} символов");
    }

    [Fact]
    public void ColorAtMaxLength_IsValid()
    {
        var cat = CreateValidCat();
        cat.Color = new string('c', CatEditValidator.MaxColorLength);

        var result = _validator.Validate(cat);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyDescription_HasRequiredError(string? description)
    {
        var cat = CreateValidCat();
        cat.Description = description!;

        var result = _validator.Validate(cat);

        AssertHasError(result, nameof(Cat.Description), "Описание обязательно");
    }

    [Fact]
    public void DescriptionLongerThanMaxLength_HasError()
    {
        var cat = CreateValidCat();
        cat.Description = new string('d', CatEditValidator.MaxDescriptionLength + 1);

        var result = _validator.Validate(cat);

        AssertHasError(
            result,
            nameof(Cat.Description),
            $"Описание не может быть свыше {CatEditValidator.MaxDescriptionLength} символов");
    }

    [Fact]
    public void DescriptionAtMaxLength_IsValid()
    {
        var cat = CreateValidCat();
        cat.Description = new string('d', CatEditValidator.MaxDescriptionLength);

        var result = _validator.Validate(cat);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyUrlPhoto_HasRequiredError(string? urlPhoto)
    {
        var cat = CreateValidCat();
        cat.UrlPhoto = urlPhoto!;

        var result = _validator.Validate(cat);

        AssertHasError(result, nameof(Cat.UrlPhoto), "Ссылка на фотографию обязательна");
    }

    [Theory]
    [InlineData("not-a-url")]
    [InlineData("/cats/barsik.jpg")]
    [InlineData("ftp://example.com/barsik.jpg")]
    [InlineData("javascript:alert(1)")]
    public void UrlPhotoWithUnsupportedFormat_HasError(string urlPhoto)
    {
        var cat = CreateValidCat();
        cat.UrlPhoto = urlPhoto;

        var result = _validator.Validate(cat);

        AssertHasError(
            result,
            nameof(Cat.UrlPhoto),
            "Укажите корректную ссылку на фотографию (http или https)");
    }

    [Theory]
    [InlineData("http://example.com/barsik.jpg")]
    [InlineData("https://example.com/barsik.jpg")]
    public void UrlPhotoWithHttpOrHttps_IsValid(string urlPhoto)
    {
        var cat = CreateValidCat();
        cat.UrlPhoto = urlPhoto;

        var result = _validator.Validate(cat);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void SeveralInvalidFields_ReturnErrorForEveryField()
    {
        var cat = new Cat
        {
            Id = 1,
            Name = string.Empty,
            Age = -5,
            Color = string.Empty,
            Description = string.Empty,
            UrlPhoto = "not-a-url"
        };

        var result = _validator.Validate(cat);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(Cat.Name));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(Cat.Age));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(Cat.Color));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(Cat.Description));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(Cat.UrlPhoto));
    }

    private static Cat CreateValidCat()
    {
        return new Cat
        {
            Id = 1,
            Name = "Барсик",
            Age = 3,
            Color = "Рыжий",
            Description = "Ласковый котик",
            UrlPhoto = "https://example.com/barsik.jpg"
        };
    }

    private static void AssertHasError(ValidationResult result, string propertyName, string errorMessage)
    {
        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == propertyName && error.ErrorMessage == errorMessage);
    }
}

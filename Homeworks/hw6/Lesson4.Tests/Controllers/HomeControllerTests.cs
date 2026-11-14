using Lesson4.Controllers;
using Lesson4.Models;
using Lesson4.Services;
using Lesson4.Tests.Infrastructure;
using Lesson4.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Lesson4.Tests.Controllers;

public class HomeControllerTests
{
    [Fact]
    public void EditGet_ExistingCat_ReturnsViewWithCat()
    {
        var repository = CreateRepository();
        var controller = CreateController(repository);

        var result = Assert.IsType<ViewResult>(controller.Edit(1));

        var model = Assert.IsType<Cat>(result.Model);
        Assert.Equal(1, model.Id);
        Assert.Equal("Барсик", model.Name);
    }

    [Fact]
    public void EditGet_MissingCat_ReturnsNotFound()
    {
        var controller = CreateController(CreateRepository());

        var result = controller.Edit(42);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void EditPost_InvalidCat_ReturnsViewAndDoesNotUpdateCat()
    {
        var repository = CreateRepository();
        var controller = CreateController(repository);
        var cat = repository.GetById(1)!;
        cat.Name = string.Empty;
        cat.UrlPhoto = "not-a-url";

        var result = Assert.IsType<ViewResult>(controller.Edit(cat));

        Assert.Same(cat, result.Model);
        Assert.False(controller.ModelState.IsValid);
        Assert.True(controller.ModelState.ContainsKey(nameof(Cat.Name)));
        Assert.True(controller.ModelState.ContainsKey(nameof(Cat.UrlPhoto)));
        Assert.Equal(0, repository.UpdateCallCount);
    }

    [Fact]
    public void EditPost_ModelStateWithBindingError_ReturnsViewAndDoesNotUpdateCat()
    {
        var repository = CreateRepository();
        var controller = CreateController(repository);
        var cat = repository.GetById(1)!;
        controller.ModelState.AddModelError(nameof(Cat.Age), "Введите число");

        var result = Assert.IsType<ViewResult>(controller.Edit(cat));

        Assert.Same(cat, result.Model);
        Assert.False(controller.ModelState.IsValid);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    [Fact]
    public void EditPost_ValidCat_UpdatesCatAndRedirectsToIndex()
    {
        var repository = CreateRepository();
        var controller = CreateController(repository);
        var cat = repository.GetById(1)!;
        cat.Name = "Мурзик";
        cat.Age = 5;

        var result = Assert.IsType<RedirectToActionResult>(controller.Edit(cat));

        Assert.Equal(nameof(HomeController.Index), result.ActionName);
        Assert.Equal(1, repository.UpdateCallCount);
        Assert.Equal("Мурзик", repository.GetById(1)!.Name);
        Assert.Equal(5, repository.GetById(1)!.Age);
    }

    [Fact]
    public void EditPost_MissingCat_ReturnsNotFoundAndDoesNotUpdateCat()
    {
        var repository = CreateRepository();
        var controller = CreateController(repository);
        var cat = new Cat
        {
            Id = 42,
            Name = "Неизвестный",
            Age = 1,
            Color = "Серый",
            Description = "Котик, которого нет в базе",
            UrlPhoto = "https://example.com/unknown.jpg"
        };

        var result = controller.Edit(cat);

        Assert.IsType<NotFoundResult>(result);
        Assert.Equal(0, repository.UpdateCallCount);
    }

    private static InMemoryCatRepository CreateRepository()
    {
        return new InMemoryCatRepository(new Cat
        {
            Id = 1,
            Name = "Барсик",
            Age = 3,
            Color = "Рыжий",
            Description = "Ласковый котик",
            UrlPhoto = "https://example.com/barsik.jpg"
        });
    }

    private static HomeController CreateController(InMemoryCatRepository repository)
    {
        return new HomeController(
            new CatService(repository),
            repository,
            new CatEditValidator());
    }
}

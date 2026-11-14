using Lesson4.Validation;

namespace Lesson4.Models;

public class AddCatPage
{
    public ValidationAddCat ValidationAddCat { get; set; } = new();
    public Cat Cat { get; set; } = new();
}

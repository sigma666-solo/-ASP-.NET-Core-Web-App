namespace Lesson4.Models;

public class Cat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string UrlPhoto { get; set; } = string.Empty;
}
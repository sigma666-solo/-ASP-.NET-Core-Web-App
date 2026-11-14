namespace Lesson4.Models;

public class RequestIndexModel
{
    public required string SearchString { get; set; }

    public int Page { get; set; } = 1;
}
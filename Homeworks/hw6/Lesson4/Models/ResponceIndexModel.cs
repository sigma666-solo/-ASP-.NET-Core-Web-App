namespace Lesson4.Models;

public class ResponceIndexModel<T>
{
    public List<T>? Pets { get; set; }
    public string? SearchString { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public ResponceIndexModel(string searchString, int currentPage, int? allCountObjects = 0, int? pageSize = 0)
    {
        SearchString = searchString;
        CurrentPage = currentPage;

        if (allCountObjects.HasValue && pageSize.HasValue)
        {
            TotalPages = (int)Math.Ceiling(allCountObjects.Value / (double)pageSize.Value);
        }
    }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

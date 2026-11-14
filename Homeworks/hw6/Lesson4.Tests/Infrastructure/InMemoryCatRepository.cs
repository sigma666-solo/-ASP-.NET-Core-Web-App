using Lesson4.Data;
using Lesson4.Models;

namespace Lesson4.Tests.Infrastructure;

internal sealed class InMemoryCatRepository : IRepository
{
    private readonly Dictionary<int, Cat> _cats;

    public InMemoryCatRepository(params Cat[] cats)
    {
        _cats = cats.ToDictionary(cat => cat.Id);
    }

    public int UpdateCallCount { get; private set; }

    public Task<List<Cat>> GetAllAsync(int countSkip, int countTake)
    {
        var cats = _cats.Values
            .OrderBy(cat => cat.Id)
            .Skip(countSkip)
            .Take(countTake)
            .ToList();

        return Task.FromResult(cats);
    }

    public Cat? GetById(int id)
    {
        return _cats.GetValueOrDefault(id);
    }

    public bool IsFavorite(int id)
    {
        return false;
    }

    public void Add(Cat cat)
    {
        _cats[cat.Id] = cat;
    }

    public void AddFavorite(int id)
    {
    }

    public void RemoveFavorite(int id)
    {
    }

    public void Remove(int id)
    {
        _cats.Remove(id);
    }

    public void Update(Cat cat)
    {
        UpdateCallCount++;
        _cats[cat.Id] = cat;
    }

    public List<Cat>? Filtered(string nameCat, int countSkip, int countTake)
    {
        var cats = _cats.Values
            .Where(cat => cat.Name.ToLower().Contains(nameCat.ToLower()))
            .OrderBy(cat => cat.Id)
            .Skip(countSkip)
            .Take(countTake)
            .ToList();

        return cats;
    }

    public Task<int> GetCountAsync(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return Task.FromResult(_cats.Count);
        }

        return Task.FromResult(
            _cats.Values.Count(cat => cat.Name.ToLower().Contains(searchString.ToLower())));
    }
}

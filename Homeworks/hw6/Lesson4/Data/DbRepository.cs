using Lesson4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson4.Data;

public class DbRepository : IRepository
{
    private readonly CatDbContext _catDbContext;

    public DbRepository(CatDbContext catDbContext)
    {
        _catDbContext = catDbContext;
    }

    public async Task<List<Cat>> GetAllAsync(int countSkip, int countTake)
    {
        return await _catDbContext.Cats.Skip(countSkip).Take(countTake).ToListAsync();
    }

    public Cat? GetById(int id)
    {
        var cat = _catDbContext.Cats.FirstOrDefault(x => x.Id == id);

        return cat;
    }

    public bool IsFavorite(int id)
    {
        //var isFavorite = favoriteCats.Any(x => x == id);

        return true;
    }

    public void Add(Cat cat)
    {
        _catDbContext.Add(cat);
        _catDbContext.SaveChanges();
    }

    public void AddFavorite(int id)
    {
        
    }

    public void RemoveFavorite(int id)
    {
        
    }

    public void Remove(int id)
    {
        var cat = _catDbContext.Cats.First(x => x.Id == id);

        _catDbContext.Remove(cat);
        _catDbContext.SaveChanges();
    }

    public void Update(Cat cat)
    {
        var updatingCat = _catDbContext.Cats.First(x => x.Id == cat.Id);

        updatingCat.Name = cat.Name;
        updatingCat.Description = cat.Description;
        updatingCat.Age = cat.Age;
        updatingCat.UrlPhoto = cat.UrlPhoto;
        updatingCat.Color = cat.Color;

        _catDbContext.SaveChanges();
    }

    public List<Cat>? Filtered(string nameCat, int countSkip, int countTake)
    {
        var cats = _catDbContext.Cats.Where(cat => cat.Name.ToLower().Contains(nameCat.ToLower()));
        cats = cats.Skip(countSkip).Take(countTake);

        return cats.ToList();
    }

    public async Task<int> GetCountAsync(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            return await _catDbContext.Cats.Where(cat => cat.Name.ToLower().Contains(searchString.ToLower())).CountAsync();
        }

        return await _catDbContext.Cats.CountAsync();
    }
}
using Lesson4.Models;

namespace Lesson4.Data;

public interface IRepository
{
    Task<List<Cat>> GetAllAsync(int countSkip, int countTake);

    Cat? GetById(int id);

    bool IsFavorite(int id);

    void Add(Cat cat);

    void AddFavorite(int id);

    void RemoveFavorite(int id);

    void Remove(int id);

    void Update(Cat cat);

    List<Cat>? Filtered(string nameCat, int countSkip, int countTake);

    Task<int> GetCountAsync(string searchString);
}
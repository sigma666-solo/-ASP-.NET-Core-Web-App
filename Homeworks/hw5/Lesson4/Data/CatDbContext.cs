using Lesson4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson4.Data;

public class CatDbContext : DbContext
{
    public DbSet<Cat> Cats { get; set; }

    public CatDbContext(DbContextOptions<CatDbContext> options) : base(options)
    {
    }
}

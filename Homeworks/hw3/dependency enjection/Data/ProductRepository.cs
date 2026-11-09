using dependency_enjection.Models;

namespace dependency_enjection.Data;

public class ProductRepository
{
    private readonly List<Product> products;

    public ProductRepository()
    {
        products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Ноутбук",
                Category = "Электроника",
                Description = "Универсальный ноутбук для учёбы и работы.",
                Price = 74999.00m,
                StockQuantity = 8,
                ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853"
            },
            new Product
            {
                Id = 2,
                Name = "Беспроводные наушники",
                Category = "Электроника",
                Description = "Наушники с чистым звуком и удобной посадкой.",
                Price = 5999.00m,
                StockQuantity = 15,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e"
            },
            new Product
            {
                Id = 3,
                Name = "Настольная лампа",
                Category = "Для дома",
                Description = "Компактная лампа для рабочего стола.",
                Price = 2499.00m,
                StockQuantity = 12,
                ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c"
            },
            new Product
            {
                Id = 4,
                Name = "Рюкзак",
                Category = "Аксессуары",
                Description = "Вместительный рюкзак для города и поездок.",
                Price = 3999.00m,
                StockQuantity = 20,
                ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62"
            },
            new Product
            {
                Id = 5,
                Name = "Термокружка",
                Category = "Для дома",
                Description = "Кружка, которая сохраняет напиток горячим.",
                Price = 1299.00m,
                StockQuantity = 30,
                ImageUrl = "https://images.unsplash.com/photo-1602143407151-7111542de6e8"
            },
            new Product
            {
                Id = 6,
                Name = "Механическая клавиатура",
                Category = "Электроника",
                Description = "Удобная клавиатура для работы и игр.",
                Price = 6999.00m,
                StockQuantity = 7,
                ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3"
            }
        };
    }

    public List<Product> GetAll()
    {
        return products;
    }
}

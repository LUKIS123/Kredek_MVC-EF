using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace JakubWiesniakLab3;

public class DbSeeder
{
    private readonly AppDbContext _context;

    public DbSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Database.CanConnect())
        {
            var pendingMigrations = _context.Database.GetPendingMigrations();
            if (pendingMigrations is not null && pendingMigrations.Any())
            {
                try
                {
                    _context.Database.Migrate();
                }
                catch (Exception)
                {
                }
            }

            if (!_context.Products.Any())
            {
                var books = GetBooks();
                _context.Products.AddRange(books);
                _context.SaveChanges();
            }

            if (!_context.OrderStatuses.Any())
            {
                var orderStatuses = GetOrderStatuses();
                _context.OrderStatuses.AddRange(orderStatuses);
                _context.SaveChanges();
            }
        }
    }

    public IEnumerable<Product> GetBooks()
    {
        var books = new List<Product>()
        {
            new Product()
            {
                Name = "Jabłko",
                Category = "Owoce",
                Price = 2.50,
                Description = "Świeże jabłko",
                ImageUrl = "apples.jpg"
            },
            new Product()
            {
                Name = "Sok", Category = "Napoje", Price = 4.99, Description = "Sok pomarańczowy 100% naturalny",
                ImageUrl = "juice.jpg"
            },
            new Product()
            {
                Name = "Książka", Category = "Książki", Price = 29.99, Description = "Najnowsza bestsellerowa książka",
                ImageUrl = "book.jpg"
            },
            new Product()
            {
                Name = "Proszek do prania", Category = "Artykuły gospodarstwa domowego", Price = 12.99,
                Description = "Proszek do prania o zapachu świeżości", ImageUrl = "detergent.jpg"
            }
        };
        return books;
    }

    public IEnumerable<OrderStatus> GetOrderStatuses()
    {
        var orderStatuses = new List<OrderStatus>()
        {
            new OrderStatus()
            {
                Name = "W koszyku"
            },
            new OrderStatus()
            {
                Name = "Złożone"
            },
        };
        return orderStatuses;
    }
}
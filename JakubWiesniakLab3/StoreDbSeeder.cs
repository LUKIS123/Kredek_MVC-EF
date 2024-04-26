using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace JakubWiesniakLab3
{
    public class StoreDbSeeder
    {
        public static readonly ICollection<ProductViewModel> Products = new List<ProductViewModel>
        {
            new ProductViewModel(1, "Jabłko", "Owoce", 2.50, "Świeże jabłko", "apples.jpg"),
            new ProductViewModel(2, "Sok", "Napoje", 4.99, "Sok pomarańczowy 100% naturalny", "juice.jpg"),
            new ProductViewModel(3, "Książka", "Książki", 29.99, "Najnowsza bestsellerowa książka", "book.jpg"),
            new ProductViewModel(4, "Proszek do prania", "Artykuły gospodarstwa domowego", 12.99,
                "Proszek do prania o zapachu świeżości", "detergent.jpg")
        };

        private readonly AppDbContext _context;

        public StoreDbSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var pending = _context.Database.GetPendingMigrations();
            if (pending is not null && pending.Any())
            {
                try
                {
                    _context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


            if (!_context.Products.Any())
            {
                var products = Products.Select(p => new Product
                {
                    Name = p.Name,
                    Category = p.Category,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                });

                _context.Products.AddRange(products);
                _context.SaveChanges();
            }

            if (!_context.OrderStatuses.Any())
            {
                _context.OrderStatuses.AddRange(new List<OrderStatus>
                {
                    new OrderStatus { Name = "W koszyku" },
                    new OrderStatus { Name = "Zrealizowano" },
                });
                _context.SaveChanges();
            }
        }
    }
}
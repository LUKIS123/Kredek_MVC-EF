using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public static readonly ICollection<Product> Products = new List<Product>
        {
            new Product(1, "Jabłko", "Owoce", 2.50, "Świeże jabłko", "apple.jpg"),
            new Product(2, "Sok", "Napoje", 4.99, "Sok pomarańczowy 100% naturalny", "juice.jpg"),
            new Product(3, "Książka", "Książki", 29.99, "Najnowsza bestsellerowa książka", "book.jpg"),
            new Product(4, "Proszek do prania", "Artykuły gospodarstwa domowego", 12.99, "Proszek do prania o zapachu świeżości", "detergent.jpg")
        };

        public Product Get(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return Products.ToList();
        }

        public void Add(Product product) 
        {
            Products.Add(product);
        }

    }
}

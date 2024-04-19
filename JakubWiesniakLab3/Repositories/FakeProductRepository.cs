using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public static readonly ICollection<ProductViewModel> Products = new List<ProductViewModel>
        {
            new ProductViewModel(1, "Jabłko", "Owoce", 2.50, "Świeże jabłko", "apples.jpg"),
            new ProductViewModel(2, "Sok", "Napoje", 4.99, "Sok pomarańczowy 100% naturalny", "juice.jpg"),
            new ProductViewModel(3, "Książka", "Książki", 29.99, "Najnowsza bestsellerowa książka", "book.jpg"),
            new ProductViewModel(4, "Proszek do prania", "Artykuły gospodarstwa domowego", 12.99,
                "Proszek do prania o zapachu świeżości", "detergent.jpg")
        };

        public ProductViewModel? Get(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return Products.ToList();
        }

        public void Add(ProductViewModel product)
        {
            Products.Add(product);
        }

        public ProductViewModel? Update(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
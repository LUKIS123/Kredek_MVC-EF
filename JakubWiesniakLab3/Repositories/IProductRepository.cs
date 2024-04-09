using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product Get(int id);

        void Add(Product product);
    }
}

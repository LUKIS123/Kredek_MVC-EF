using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Products
{
    public interface IProductRepository
    {
        IEnumerable<ProductViewModel> GetAll();

        ProductViewModel? Get(int id);

        void Add(ProductViewModel product);

        bool Update(ProductViewModel product);

        void Delete(int id);
    }
}
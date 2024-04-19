using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductViewModel> GetAll();

        ProductViewModel? Get(int id);

        void Add(ProductViewModel product);

        ProductViewModel? Update(ProductViewModel product);

        void Delete(int id);
    }
}
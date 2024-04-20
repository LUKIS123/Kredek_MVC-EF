using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public ProductViewModel? Get(int id)
    {
        return _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            })
            .FirstOrDefault();
    }

    public IEnumerable<ProductViewModel> GetAll()
    {
        return _context.Products
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            })
            .ToList();
    }

    public void Add(ProductViewModel product)
    {
        _context.Products.Add(new DataAccess.Entities.Product
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        });
        _context.SaveChanges();
    }

    public bool Update(ProductViewModel product)
    {
        var productToUpdate = _context.Products
            .FirstOrDefault(x => x.Id == product.Id);

        if (productToUpdate is null)
        {
            return false;
        }

        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Category = product.Category;
        productToUpdate.Price = product.Price;
        productToUpdate.ImageUrl = product.ImageUrl;

        _context.SaveChanges();
        return true;
    }

    public void Delete(int id)
    {
        var productToDelete = _context.Products
            .FirstOrDefault(x => x.Id == id);

        if (productToDelete is not null)
        {
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }
    }
}
using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductViewModel> GetAll()
    {
        return _context.Products
            .Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            }).ToList();
    }

    public ProductViewModel? Get(int id)
    {
        var firstOrDefault = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (firstOrDefault is null)
        {
            return null;
        }

        return new ProductViewModel()
        {
            Id = firstOrDefault.Id,
            Name = firstOrDefault.Name,
            Category = firstOrDefault.Category,
            Price = firstOrDefault.Price,
            Description = firstOrDefault.Description,
            ImageUrl = firstOrDefault.ImageUrl
        };
    }

    public void Add(ProductViewModel product)
    {
        _context.Products.Add(new Product
        {
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        });

        _context.SaveChanges();
    }

    public bool Update(ProductViewModel product)
    {
        var productToUpdate = _context.Products
            .FirstOrDefault(p => p.Id == product.Id);

        if (productToUpdate is null)
        {
            return false;
        }

        productToUpdate.Name = product.Name;
        productToUpdate.Category = product.Category;
        productToUpdate.Price = product.Price;
        productToUpdate.Description = product.Description;
        productToUpdate.ImageUrl = product.ImageUrl;

        _context.SaveChanges();

        return true;
    }

    public void Delete(int id)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return;
        }

        _context.Products.Remove(product);

        _context.SaveChanges();
    }
}
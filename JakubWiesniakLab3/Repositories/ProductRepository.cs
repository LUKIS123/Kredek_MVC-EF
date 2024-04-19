using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories;

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
        _context.Products.Add(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        });
        _context.SaveChanges();
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
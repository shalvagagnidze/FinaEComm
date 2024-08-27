using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(ECommerceDbContext db)
    {
        var dbSet = db.Set<Product>();
        _dbSet = dbSet;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _dbSet.FindAsync(id);

        return product!;
    }

    public async Task AddAsync(Product product)
    {
        await _dbSet.AddAsync(product);
    }

    public void Delete(Product product)
    {
        product.DeleteProduct();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var product = await _dbSet.FindAsync(id);

        product!.DeleteProduct();
    }

    public void Update(Product product)
    {
        _dbSet.Update(product);
    }
}

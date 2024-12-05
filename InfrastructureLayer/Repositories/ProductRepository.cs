using DomainLayer.Entities;
using DomainLayer.Entities.Products;
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
        return await _dbSet.AsNoTracking().Include(category => category.Category)
            .Include(brand => brand.Brand)
            .Include(product => product.ProductFacetValues)
            .ThenInclude(product => product.FacetValue)
            .ThenInclude(product => product.Facet).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {

        var product = await _dbSet.AsNoTracking().Include(category => category.Category)
                            .Include(brand => brand.Brand)
                            .Include(product => product.ProductFacetValues)
                            .ThenInclude(product => product.FacetValue)
                            .ThenInclude(product => product.Facet)
                            .FirstOrDefaultAsync(prod => prod.Id == id);
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

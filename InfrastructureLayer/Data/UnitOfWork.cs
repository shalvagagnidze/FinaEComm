using DomainLayer.Interfaces;
using InfrastructureLayer.Repositories;

namespace InfrastructureLayer.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _db;
    private readonly BrandRepository _brandRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly ProductRepository _productRepository;

    public UnitOfWork(ECommerceDbContext db)
    {
        _db = db;
        _brandRepository = new BrandRepository(_db);
        _categoryRepository = new CategoryRepository(_db);
        _productRepository = new ProductRepository(_db);
    }

    public IBrandRepository BrandRepository => _brandRepository;
    public ICategoryRepository CategoryRepository => _categoryRepository;
    public IProductRepository ProductRepository => _productRepository;
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}

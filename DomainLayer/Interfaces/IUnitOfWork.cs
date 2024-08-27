namespace DomainLayer.Interfaces;

public interface IUnitOfWork
{
    IBrandRepository BrandRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    Task SaveAsync();
}

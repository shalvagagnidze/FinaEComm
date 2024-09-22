namespace DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IFacetRepository FacetRepository { get; }
        IFacetValueRepository FacetValueRepository { get; }
        IProductFacetValueRepository ProductFacetValueRepository { get; }
        Task SaveAsync();
    }
}

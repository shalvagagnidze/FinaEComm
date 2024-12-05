using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class ProductFacetValueRepository : IProductFacetValueRepository
    {
        private readonly DbSet<ProductFacetValue> _dbSet;

        public ProductFacetValueRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<ProductFacetValue>();
            _dbSet = dbSet;
        }

        public async Task AddAsync(ProductFacetValue entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(ProductFacetValue entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<ProductFacetValue>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<ProductFacetValue> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(fa => fa.Id == id);
        }

        public void Update(ProductFacetValue entity)
        {
            _dbSet.Update(entity);
        }
    }
}

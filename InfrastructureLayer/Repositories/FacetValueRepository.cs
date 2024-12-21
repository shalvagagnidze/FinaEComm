using DomainLayer.Entities.Facets;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    public class FacetValueRepository : IFacetValueRepository
    {
        private readonly DbSet<FacetValue> _dbSet;

        public FacetValueRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<FacetValue>();
            _dbSet = dbSet;
        }

        public async Task AddAsync(FacetValue entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(FacetValue entity)
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

        public async Task<IEnumerable<FacetValue>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<FacetValue> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(FacetValue entity)
        {
            _dbSet.Update(entity);
        }

        public async Task AddOrUpdateAsync(FacetValue entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Entry(entity).CurrentValues.SetValues(entity);
            }
        }
    }
}

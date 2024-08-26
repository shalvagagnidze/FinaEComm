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
    public class FacetRepository : IFacetRepository
    {
        private readonly DbSet<Facet> _dbSet;

        public FacetRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<Facet>();
            _dbSet = dbSet;
        }

        public async Task AddAsync(Facet entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(Facet entity)
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

        public async Task<IEnumerable<Facet>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Facet> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(Facet entity)
        {
            _dbSet.Update(entity);
        }
    }
}

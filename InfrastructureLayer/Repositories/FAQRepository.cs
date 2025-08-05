using DomainLayer.Entities;
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
    public class FAQRepository : IFAQRepository
    {
        private readonly DbSet<FAQ> _dbSet;

        public FAQRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<FAQ>();
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<FAQ>> GetAllAsync()
        {
            return await _dbSet.OrderBy(x => x.OrderNum).ToListAsync();
        }

        public async Task<FAQ> GetByIdAsync(Guid id)
        {
            var FAQ = await _dbSet.FindAsync(id);

            return FAQ!;
        }

        public async Task AddAsync(FAQ entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(FAQ entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(FAQ entity)
        {
            _dbSet.Remove(entity);  
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var FAQ = await _dbSet.FindAsync(id);

            _dbSet.Remove(FAQ!);
        }

    }
}

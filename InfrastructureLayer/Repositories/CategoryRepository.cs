using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities.Facets;

namespace InfrastructureLayer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<Category>();
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbSet.Include(cat => cat.Facets).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _dbSet.Include(ca => ca.Facets).FirstOrDefaultAsync(fa => fa.Id == id);

            return category!;
        }

        public async Task AddAsync(Category category)
        {
            await _dbSet.AddAsync(category);
        }

        public void Delete(Category category)
        {
            category.DeleteCategory();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var category = await _dbSet.FindAsync(id);

            category!.DeleteCategory();
        }

        public void Update(Category category)
        {
            _dbSet.Update(category);
        }
    }
}

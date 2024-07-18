using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DbSet<Brand> _dbSet;

        public BrandRepository(ECommerceDbContext db)
        {
            var dbSet = db.Set<Brand>();
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(Guid id)
        {
            var brand = await _dbSet.FindAsync(id);

            return brand!;
        }

        public async Task AddAsync(Brand brand)
        {
            await _dbSet.AddAsync(brand);
        }

        public void Delete(Brand brand)
        {
            _dbSet.Remove(brand);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var brand = await _dbSet.FindAsync(id);

            _dbSet.Remove(brand!);
        }

        public void Update(Brand brand)
        {
            _dbSet.Update(brand);
        }
    }
}

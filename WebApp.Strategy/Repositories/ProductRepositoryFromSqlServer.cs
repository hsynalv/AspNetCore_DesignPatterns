using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdentityDbContext _dbContext;

        public ProductRepositoryFromSqlServer(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _dbContext.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> Save(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

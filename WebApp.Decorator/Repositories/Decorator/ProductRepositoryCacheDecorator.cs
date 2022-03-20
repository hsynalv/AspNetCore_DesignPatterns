using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;
        private const string ProductRepositoryCacheKey = "products";

        public ProductRepositoryCacheDecorator(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
        {
            _memoryCache = memoryCache;
        }

        public async override Task<List<Product>> GetAll()
        {
            if (_memoryCache.TryGetValue(ProductRepositoryCacheKey,out List<Product> cacheProducts))
            {
                return cacheProducts;
            }

            UpdateCache();

            return _memoryCache.Get<List<Product>>(ProductRepositoryCacheKey); 
        }


        public async override Task<List<Product>> GetAll(string userId)
        {
            var products = await GetAll();
            return products.Where(x => x.UserId == userId).ToList();
        }

        public async override Task<Product> Save(Product product)
        {
            await base.Save(product);

            UpdateCache();
            return product;
        }

        public async override Task Update(Product product)
        {
            await base.Update(product);

            UpdateCache();
        }

        public async override Task Remove(Product product)
        {
            await base.Remove(product);
            UpdateCache();
        }


        private async void UpdateCache()
        {
            _memoryCache.Set(ProductRepositoryCacheKey, await base.GetAll());
        }
    }


}

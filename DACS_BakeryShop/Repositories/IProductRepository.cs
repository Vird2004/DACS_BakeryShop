using DACS_BakeryShop.Models;
using System.Linq.Expressions;

namespace DACS_BakeryShop.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);

        public Task<List<Product>> GetFilteredAsync(Expression<Func<Product, bool>> filter, bool includeCategory = false);

        Task<List<ProductImage>> GetProductImagesAsync(int productId);
    }
}

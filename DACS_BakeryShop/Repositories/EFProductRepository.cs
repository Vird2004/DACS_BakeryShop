using DACS_BakeryShop.DataAccess;
using DACS_BakeryShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace DACS_BakeryShop.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync(); 
            return await _context.Products
        .Include(p => p.Category) // Include thông tin về category 
        .Include(p => p.ProductImages)
        .ToListAsync();

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id); 
            // lấy thông tin kèm theo category 
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetFilteredAsync(Expression<Func<Product, bool>> filter, bool includeCategory = false)
        {
            var query = _context.Products.AsQueryable();

            if (includeCategory)
            {
                query = query.Include(p => p.Category);
            }

            return await query.Where(filter).ToListAsync();
        }

        public async Task<List<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _context.ProductImages
                .Where(img => img.ProductId == productId)
                .ToListAsync();
        }

    }
}
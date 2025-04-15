using DACS_BakeryShop.Models;
using DACS_BakeryShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace DACS_BakeryShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,
ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        //Hien thi danh sach san pham

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        //Hien thi form them san pham moi


        //Xu ly them san pham moi





        //Hien thi thong tin chi tiet san pham
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Hien thi form cap nhat san pham


        //Xu ly cap nhat san pham


        // Hiển thị form xác nhận xóa sản phẩm 


        public async Task<IActionResult> Search(string keyword)
        {
            var products = string.IsNullOrEmpty(keyword)
                ? await _productRepository.GetAllAsync()
                : await _productRepository.GetFilteredAsync(
                    p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase),
                    includeCategory: true
                );

            return View("Index", products);
        }


        public async Task<List<Product>> GetFilteredAsync(Expression<Func<Product, bool>> filter)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Where(filter.Compile()).ToList();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.Services;
using System.IO;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiClient _api;
        private readonly string _baseUrl = "http://localhost:5022/api/product"; // API Gateway URL

        public ProductController(ApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _api.GetAsync<List<Product>>(_baseUrl);
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (product.ProductImage != null && product.ProductImage.Length > 0)
            {
                var fileName = Path.GetFileName(product.ProductImage.FileName);
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsDir);

                var savePath = Path.Combine(uploadsDir, fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await product.ProductImage.CopyToAsync(stream);
                }

                // Set the image URL for API (you can adjust this path according to your API expectations)
                product.ProductImageUrl = "/uploads/" + fileName;
            }

            var response = await _api.PostAsync(_baseUrl, product);
            TempData["message"] = response.IsSuccessStatusCode ? "success" : "error";
            return RedirectToAction(nameof(Index));
        }
    }
}

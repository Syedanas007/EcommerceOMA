using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.Services;

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
        public async Task<IActionResult> Create(Product product)
        {
            var response = await _api.PostAsync(_baseUrl, product);
            TempData["message"] = response.IsSuccessStatusCode ? "success" : "error";
            return RedirectToAction(nameof(Index));
        }
    }
}

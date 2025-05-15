using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.Services;

namespace UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApiClient _api;
        private readonly string _baseUrl = "http://localhost:5022/api/category"; // API Gateway URL

        public CategoryController(ApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _api.GetAsync<List<Category>>(_baseUrl);
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var response = await _api.PostAsync(_baseUrl, category);
            if (response.IsSuccessStatusCode)
                TempData["message"] = "success";
            else
                TempData["message"] = "error";

            return RedirectToAction(nameof(Index));
        }
    }
}

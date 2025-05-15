using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.Services;

namespace UI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApiClient _api;
        private readonly string _baseUrl = "http://localhost:5022/api/company"; // API Gateway URL

        public CompanyController(ApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _api.GetAsync<List<Company>>(_baseUrl);
            return View(companies);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            var response = await _api.PostAsync(_baseUrl, company);
            TempData["message"] = response.IsSuccessStatusCode ? "success" : "error";
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _baseUrl = "http://localhost:5022/api/product"; // API Gateway URL

        // GET: /Product/Create
        public IActionResult Create() => View();

        // GET: /Product/Index
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {response.StatusCode}");
                return View(new List<Product>());
            }

            var products = await response.Content.ReadFromJsonAsync<List<Product>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Console.WriteLine(JsonSerializer.Serialize(products));

            return View(products);
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            // Add image file if present
            if (product.ProductImageUrl != null && product.ProductImageUrl.Length > 0)
            {
                var streamContent = new StreamContent(product.ProductImageUrl.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(product.ProductImageUrl.ContentType);

                form.Add(streamContent, "ProductImage", product.ProductImageUrl.FileName);
            }

            // Add other product fields
            form.Add(new StringContent(product.ProductName ?? ""), "ProductName");
            form.Add(new StringContent(product.ProductCategoryName ?? ""), "ProductCategoryName");
            form.Add(new StringContent(product.Manufacturer ?? ""), "Manufacturer");
            form.Add(new StringContent(product.Quantity.ToString()), "Quantity");
            form.Add(new StringContent(product.Price.ToString()), "Price");

            var response = await client.PostAsync(_baseUrl, form);

            TempData["message"] = response.IsSuccessStatusCode ? "success" : "error";
            return RedirectToAction(nameof(Index));
        }
    }
}

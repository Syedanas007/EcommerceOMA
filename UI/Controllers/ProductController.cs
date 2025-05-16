using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _baseUrl = "http://localhost:5022/api/product";
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            var token = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


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

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                 using var client = new HttpClient();

        // ✅ Get token from session
        var token = HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError(string.Empty, "Unauthorized: JWT token is missing.");
            return View(product);
        }

        // ✅ Add the token to the Authorization header
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Convert Quantity and Price to string using member assignment
        var productDto = new
        {
            product.ProductName,
            product.ProductCategoryName,
            product.Manufacturer,
            Quantity = product.Quantity.ToString(),
            Price = product.Price.ToString(),
            product.ProductImage
        };

        var json = JsonSerializer.Serialize(productDto);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_baseUrl, content);
        Console.WriteLine($"Response StatusCode: {response.StatusCode}");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body: {responseBody}");
        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Product created successfully!";
            TempData["MessageType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Failed to create product: {response.StatusCode}, {errorContent}");
            return View(product);
        }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
                return View(product);
            }
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {response.StatusCode}");
                return RedirectToAction(nameof(Index));
            }

            var product = await response.Content.ReadFromJsonAsync<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {response.StatusCode}");
                return RedirectToAction(nameof(Index));
            }

            var product = await response.Content.ReadFromJsonAsync<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync($"{_baseUrl}/{id}");

            TempData["message"] = response.IsSuccessStatusCode ? "deleted" : "delete-error";
            return RedirectToAction(nameof(Index));
        }
    }
}

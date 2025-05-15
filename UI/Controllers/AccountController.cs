using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var loginModel = new { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Replace with your actual Auth API URL
            var response = await _httpClient.PostAsync("http://localhost:5001/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);

                // Store token in session
                HttpContext.Session.SetString("JWToken", tokenObj.Token);

                // Redirect to homepage or dashboard after successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login");
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}

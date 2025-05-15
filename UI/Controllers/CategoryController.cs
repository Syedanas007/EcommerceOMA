[Authorize]
public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
                var token = HttpContext.Session.GetString("JWTToken");
        if (string.IsNullOrEmpty(token)) return RedirectToAction("login", "auth");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync("/category/api/category");
        var json = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<Category>>(json);
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/category/api/category", content);
        
        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Category created successfully.";
        }
        else
        {
            TempData["Error"] = "Failed to create category.";
        }

        return RedirectToAction("Index");
    }
}

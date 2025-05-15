[Authorize]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public ProductController(IHttpClientFactory factory, IConfiguration config)
    {
        _httpClientFactory = factory;
        _config = config;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWTToken");
        if (string.IsNullOrEmpty(token)) return RedirectToAction("login", "auth");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"{_config["ApiGatewayBaseUrl"]}/product/api/product");
        if (!response.IsSuccessStatusCode) return View(new List<Product>());

        var json = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<Product>>(json);
        return View(products);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Product model)
    {
        var token = HttpContext.Session.GetString("JWTToken");
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{_config["ApiGatewayBaseUrl"]}/product/api/product", content);

        TempData[response.IsSuccessStatusCode ? "Success" : "Error"] = response.IsSuccessStatusCode
            ? "Product created successfully."
            : "Failed to create product.";
        return RedirectToAction("Index");
    }
}

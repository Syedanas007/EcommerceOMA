[Authorize]

public class CompanyController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public CompanyController(IHttpClientFactory factory, IConfiguration config)
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

        var response = await client.GetAsync($"{_config["ApiGatewayBaseUrl"]}/company/api/company");
        if (!response.IsSuccessStatusCode) return View(new List<Company>());

        var json = await response.Content.ReadAsStringAsync();
        var companies = JsonConvert.DeserializeObject<List<Company>>(json);
        return View(companies);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Company model)
    {
        var token = HttpContext.Session.GetString("JWTToken");
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{_config["ApiGatewayBaseUrl"]}/company/api/company", content);

        TempData[response.IsSuccessStatusCode ? "Success" : "Error"] = response.IsSuccessStatusCode
            ? "Company created successfully."
            : "Failed to create company.";
        return RedirectToAction("Index");
    }
}

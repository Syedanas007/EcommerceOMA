using CategoryService.Data;
using CategoryService.Factory;
using CategoryService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CategoryServiceImpl>();
builder.Services.AddScoped<ICategoryFactory, CategoryFactory>();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Category API", // Title for your API
        Version = "v1", // Version of the API
        Description = "A simple API for Category management." // Optional: API description
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Swagger middleware should be added here, before other middlewares
    app.UseDeveloperExceptionPage();

    // Register the Swagger middleware
    app.UseSwagger(); // This will serve the Swagger JSON at /swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Category API V1"); // Swagger UI URL
        c.RoutePrefix = string.Empty; // Optional: Set Swagger UI to be accessible at root URL (default is /swagger)
    });
}

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization
app.MapControllers(); // Map controllers to endpoints

app.Run(); // 
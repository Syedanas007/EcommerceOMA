using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Models;
using CategoryService.Services;
using CategoryService.Factory;

namespace CategoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServiceImpl _service;
        private readonly ICategoryFactory _factory;

        public CategoryController(CategoryServiceImpl service, ICategoryFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        // GET all categories
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        // ✅ Get a single category by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _service.GetByIdAsync(id);
            
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }


        // POST new category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.CategoryName) || category.Orders < 1)
            {
                return BadRequest("Name and orders are required.");
            }

            // Create category using factory
            var cat = _factory.CreateCategory(category.CategoryName, category.Orders);
            await _service.AddAsync(cat);
            return Ok(cat);
        }

        // PUT (update) category
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Category updated)
        {
            updated.Id = id;
            await _service.UpdateAsync(updated);
            return Ok();
        }

        // DELETE category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompanyService.Models;
using CompanyService.Services;
using CompanyService.Factory;
using System.ComponentModel.DataAnnotations;

namespace CompanyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyServiceImpl _service;
        private readonly ICompanyFactory _factory;

        public CompanyController(CompanyServiceImpl service, ICompanyFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        // ✅ Get all companies
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await _service.GetAllAsync();
            return Ok(companies);
        }

        // ✅ Get a single company by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _service.GetByIdAsync(id);
            if (company == null)
                return NotFound($"No company found with ID {id}");

            return Ok(company);
        }


        // ✅ Add a new company (Uses JSON body with validation)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = _factory.CreateCompany(dto.Name, dto.StreetAddress, dto.City, dto.State, dto.PostalAddress, dto.Zip, dto.ContactNumber);
            await _service.AddAsync(company);
            return Ok(company);
        }

        // ✅ Update a company
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Company updatedCompany)
        {
            updatedCompany.Id = id;
            await _service.UpdateAsync(updatedCompany);
            return Ok();
        }

        // ✅ Delete a company
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
        public class CreateCompanyDto
    {
        [Required] public string Name { get; set; }
        [Required] public string StreetAddress { get; set; }
        [Required] public string City { get; set; }
        [Required] public string State { get; set; }
        [Required] public string PostalAddress { get; set; }
        [Required] public string Zip { get; set; }
        [Required] public string ContactNumber { get; set; }
    }
}

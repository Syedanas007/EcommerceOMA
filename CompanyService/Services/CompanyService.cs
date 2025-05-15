using CompanyService.Data;
using CompanyService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Services
{
    public class CompanyServiceImpl
    {
        private readonly ApplicationDbContext _context;

        public CompanyServiceImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all companies
        public async Task<List<Company>> GetAllAsync() => await _context.Companies.ToListAsync();

        // Get company by ID
        public async Task<Company> GetByIdAsync(Guid id) => await _context.Companies.FindAsync(id);

        // Add a company
        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        // Update a company
        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        // Delete a company
        public async Task DeleteAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}

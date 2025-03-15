using Gestionnaire_Collections.Data;
using Gestionnaire_Collections.DTO.Shared;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire_Collections.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetPrincipalCategoriesAsync();
    }
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetPrincipalCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.Is_principal && c.Is_active)
                .Select(c => new CategoryDTO
                {
                    Name = c.Name,
                    Is_active = c.Is_active,
                    Is_principal = c.Is_principal
                })
                .ToListAsync();
        }
    }
}

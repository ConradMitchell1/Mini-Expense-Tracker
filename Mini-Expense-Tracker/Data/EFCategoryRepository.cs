using Microsoft.EntityFrameworkCore;
using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Data
{
    public class EFCategoryRepository : ICategoryReader
    {
        private readonly AppDbContext _db;
        public EFCategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            var categories = await _db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
            return categories;
        }

    }
}

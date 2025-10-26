using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReader _reader;
        public CategoryService(ICategoryReader reader)
        {
            _reader = reader;
        }
        public Task<IReadOnlyList<Category>> GetAllAsync()
        {
            var categories = _reader.GetAllAsync();
            return categories;
        }
    }
}

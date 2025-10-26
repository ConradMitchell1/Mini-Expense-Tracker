using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Services.Interfaces
{
    public interface ICategoryReader
    {
        Task<IReadOnlyList<Category>> GetAllAsync();
    }
}

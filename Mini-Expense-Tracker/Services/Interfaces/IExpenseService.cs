using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<AddExpenseResult> AddAsync(Expense expense);
        Task<IReadOnlyList<Expense>> GetAsync(DateOnly? month = null, string? category = null);
        Task DeleteAsync(int id);
        Task<byte[]> ExportAsync(string format, DateOnly? month = null, string? category = null);
    }
}

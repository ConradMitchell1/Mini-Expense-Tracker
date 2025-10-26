using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Services.Interfaces
{
    public interface IExpenseReader
    {
        Task<IReadOnlyList<Expense>> GetExpensesAsync(DateOnly? month = null, string? category = null);
        Task<decimal> SumAsync(DateOnly month);
    }
}

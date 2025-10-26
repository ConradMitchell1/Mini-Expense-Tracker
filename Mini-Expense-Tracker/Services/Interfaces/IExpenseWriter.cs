using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Services.Interfaces
{
    public interface IExpenseWriter
    {
        Task AddAsync(Expense e);
        Task DeleteAsync(int id);
    }
}

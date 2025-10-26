using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services.Budget
{
    public class CategoryBudgetRule : IBudgetRule
    {
        public Task<BudgetStatus> EvaluateAsync(DateOnly month)
        {
            throw new NotImplementedException();
        }
    }
}

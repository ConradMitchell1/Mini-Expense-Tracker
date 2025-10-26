using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services.Budget
{
    public class MonthlyBudgetRule : IBudgetRule
    {
        private readonly IExpenseReader _reader;
        private readonly decimal _monthlyLimit;
        public MonthlyBudgetRule(IExpenseReader reader, decimal monthlyLimit = 500m)
        {
            _reader = reader;
            _monthlyLimit = monthlyLimit;
        }
        public async Task<BudgetStatus> EvaluateAsync(DateOnly month)
        {
            var spentTask = await _reader.SumAsync(month);
            var exceeded = spentTask > _monthlyLimit;
            return new BudgetStatus(exceeded, _monthlyLimit, spentTask, exceeded ? "Monthly budget exceeded." : null);
        }
    }
}

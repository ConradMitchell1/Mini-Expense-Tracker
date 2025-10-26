using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseReader _reader;
        private readonly IExpenseWriter _writer;
        private readonly IEnumerable<IExporter> _exporters;
        private readonly IBudgetRule _budgetRule;
        public ExpenseService(
            IExpenseReader reader,
            IExpenseWriter writer,
            IEnumerable<IExporter> exporters,
            IBudgetRule budgetRule)
        {
            _reader = reader;
            _writer = writer;
            _exporters = exporters;
            _budgetRule = budgetRule;
        }
        public async Task<AddExpenseResult> AddAsync(Expense expense)
        {
            await _writer.AddAsync(expense);

            var month = DateOnly.FromDateTime(expense.When == default ? DateTime.UtcNow : expense.When);
            var status = await _budgetRule.EvaluateAsync(month);
            return new AddExpenseResult
            {
                BudgetExceeded = status.IsExceeded,
                Status = status
            };
        }

        public async Task<byte[]> ExportAsync(string format, DateOnly? month = null, string? category = null)
        {
            var data = await _reader.GetExpensesAsync(month, category);
            var exporter = _exporters.First(e => e.FileExtension.Equals($".{format}", StringComparison.OrdinalIgnoreCase));
            return exporter.Export(data);
        }

        public async Task DeleteAsync(int id)
        {
            await _writer.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Expense>> GetAsync(DateOnly? month = null, string? category = null)
        {
            return await _reader.GetExpensesAsync(month, category);
        }
    }
}

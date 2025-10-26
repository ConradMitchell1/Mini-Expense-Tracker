using Microsoft.EntityFrameworkCore;
using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Data
{
    public class EfExpenseRepository : IExpenseReader, IExpenseWriter
    {
        private readonly AppDbContext _db;
        public EfExpenseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Expense e)
        {
            _db.Expenses.Add(e);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _db.Expenses.FindAsync(id);
            if (expense != null)
            {
                _db.Expenses.Remove(expense);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Expense>> GetExpensesAsync(DateOnly? month = null, string? category = null)
        {
            IQueryable<Expense> q = _db.Expenses.AsNoTracking().Include(e => e.Category);

            if(month is not null)
            {
                var start = month.Value.ToDateTime(TimeOnly.MinValue);
                var end = start.AddMonths(1);
                q = q.Where(e => e.When >= start && e.When < end);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                q = q.Where(e => e.Category.Name.ToLower() == category.ToLower());
            }

            return await q.OrderByDescending(e => e.When).ToListAsync();
        }

        public async Task<decimal> SumAsync(DateOnly month)
        {
            var start = month.ToDateTime(TimeOnly.MinValue);
            var end = start.AddMonths(1);
            return await _db.Expenses
                .Where(e => e.When >= start && e.When < end)
                .SumAsync(e => e.Amount);
        }
    }
}

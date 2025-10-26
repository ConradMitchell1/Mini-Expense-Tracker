namespace Mini_Expense_Tracker.Models
{
    public class AddExpenseResult
    {
        public bool BudgetExceeded { get; set; }

        public BudgetStatus Status { get; set; } = default!;
    }
}

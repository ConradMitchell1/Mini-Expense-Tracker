namespace Mini_Expense_Tracker.Models
{
    public record BudgetStatus(
        bool IsExceeded,
        decimal Limit,
        decimal Spent,
        string? Details = null
        );

}

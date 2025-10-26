namespace Mini_Expense_Tracker.Services.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAsync(string message);
    }
}

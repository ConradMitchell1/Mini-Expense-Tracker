using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Services.Interfaces
{
    // O Open/Closed Principle: This interface can be extended with new exporters without modifying existing code.
    public interface IExporter
    {
        string ContentType { get; }
        string FileExtension { get; }
        byte[] Export(IEnumerable<Expense> expenses);
    }

}

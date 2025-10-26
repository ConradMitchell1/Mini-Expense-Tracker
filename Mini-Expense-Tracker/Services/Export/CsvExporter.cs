using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services.Export
{
    public class CsvExporter : IExporter
    {
        public string ContentType => "text/csv";

        public string FileExtension => ".csv";

        public byte[] Export(IEnumerable<Expense> expenses)
        {
            var lines = new List<string> {"When, Category, Amount, Note"};
            lines.AddRange(expenses.Select(e => $"{e.When:yyyy-MM-dd}, {e.Category}, {e.Amount}, {e.Note?.Replace(',', ';')}"));
            return System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
        }
    }
}

using Mini_Expense_Tracker.Models;
using Mini_Expense_Tracker.Services.Interfaces;

namespace Mini_Expense_Tracker.Services.Export
{
    public class JsonExporter : IExporter
    {
        public string ContentType => "application/json";

        public string FileExtension => ".json";

        public byte[] Export(IEnumerable<Expense> expenses)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(expenses);
            
        }
    }
}

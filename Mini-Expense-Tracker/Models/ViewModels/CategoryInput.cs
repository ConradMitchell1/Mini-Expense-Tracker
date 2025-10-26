using System.ComponentModel.DataAnnotations;

namespace Mini_Expense_Tracker.Models.ViewModels
{
    public class CategoryInput
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}

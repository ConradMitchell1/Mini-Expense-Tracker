using System.ComponentModel.DataAnnotations;

namespace Mini_Expense_Tracker.Models.ViewModels
{
    public class ExpenseInput
    {
        [Required]
        public DateTime When { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Note { get; set; }

        public IEnumerable<CategoryInput>? Categories { get; set; }
        public Expense ToEntity() =>
            new()
            {
                When = this.When == default ? DateTime.UtcNow : this.When,
                Amount = Amount,
                CategoryId = CategoryId,
                Description = Description,
                Note = Note
            };

    }
}

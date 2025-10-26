using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Expense_Tracker.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime When { get; set; } = DateTime.UtcNow;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = default!;
        public string? Note { get; set; }

        
    }
}

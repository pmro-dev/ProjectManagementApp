#region USINGS
using App.Features.Budgets.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Common.Interfaces;

public interface IBillingBase
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public long Value { get; set; }

    [Required]
    public Guid BudgetId { get; set; }

    [Required]
    [ForeignKey(nameof(BudgetId))]
    public BudgetModel? Budget { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? PaymentDeadline { get; set; }
}

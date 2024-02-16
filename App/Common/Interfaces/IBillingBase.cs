#region USINGS
using App.Features.Budgets.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Common.Interfaces;

public interface IBillingBase
{
    [Required]
    [Key]
    Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
    string Name { get; set; }

    [Required]
    string Description { get; set; }

    [Required]
    long Value { get; set; }

	string? ExecutorId { get; set; }

    //[ForeignKey(nameof(ExecutorId))]
    //UserModel? Executor { get; set; }

    [Required]
    Guid BudgetId { get; set; }

    [Required]
    [ForeignKey(nameof(BudgetId))]
    BudgetModel? Budget { get; set; }

    DateTime? PaymentDate { get; set; }

    DateTime? PaymentDeadline { get; set; }
}

using App.Features.Budgets.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Common.Interfaces;

public interface IBillingBaseDto
{
    Guid Id { get; set; }

    byte[] RowVersion { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    long Value { get; set; }

    string ExecutorId { get; set; }

    UserModel? Executor { get; set; }

    Guid BudgetId { get; set; }

    BudgetModel? Budget { get; set; }

    DateTime? PaymentDate { get; set; }

    DateTime? PaymentDeadline { get; set; }
}

﻿using App.Features.Budgets.Common.Models;
using App.Features.Incomes.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Incomes.Common.Models;

public class IncomeModel : IIncomeModel
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	[Timestamp]
	public byte[] RowVersion { get; set; }

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
	public virtual BudgetModel? Budget { get; set; }

	public string? ExecutorId { get; set; }

	public DateTime? PaymentDate { get; set; }

	public DateTime? PaymentDeadline { get; set; }

    public IncomeModel()
    {
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Name = string.Empty;
		Description = string.Empty;
		Value = 0;
		BudgetId = Guid.Empty;
		PaymentDeadline = null;
		PaymentDate = null;
		ExecutorId = string.Empty;
	}

    public IncomeModel(string name, string description, long value, Guid budgetId, string executorId, DateTime? paymentDeadline = null, DateTime? paymentDate = null)
	{
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Name = name;
		Description = description;
		Value = value;
		BudgetId = budgetId;
		PaymentDate = paymentDate;
		PaymentDeadline = paymentDeadline;
		ExecutorId = executorId;
	}
}
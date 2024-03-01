using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Budgets.Edit;

public class EditBudgetHandler :
	IRequestHandler<EditBudgetQuery, EditBudgetQueryResponse>
	IRequestHandler<EditBudgetCommand, EditBudgetCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IBudgetRepository _budgetRepository;
	private readonly ILogger<EditBudgetHandler> _logger;

	public EditBudgetHandler(IDataUnitOfWork dataUnitOfWork, ILogger<EditBudgetHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_budgetRepository = _dataUnitOfWork.BudgetRepository;
		_logger = logger;
	}

	public Task<EditBudgetQueryResponse> Handle(EditBudgetQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<EditBudgetCommandResponse> Handle(EditBudgetCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

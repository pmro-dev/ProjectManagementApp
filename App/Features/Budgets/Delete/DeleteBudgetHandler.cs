using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Budgets.Delete;

public class DeleteBudgetHandler :
	IRequestHandler<DeleteBudgetQuery, DeleteBudgetQueryResponse>,
	IRequestHandler<DeleteBudgetCommand, DeleteBudgetCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IBudgetRepository _budgetRepository;
	private readonly ILogger<DeleteBudgetHandler> _logger;

	public DeleteBudgetHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteBudgetHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_budgetRepository = _dataUnitOfWork.BudgetRepository;
		_logger = logger;
	}

	public Task<DeleteBudgetQueryResponse> Handle(DeleteBudgetQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteBudgetCommandResponse> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

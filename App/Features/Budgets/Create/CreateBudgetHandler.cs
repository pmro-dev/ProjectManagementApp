using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Budgets.Create;

public class CreateBudgetHandler :
	IRequestHandler<CreateBudgetQuery, CreateBudgetQueryResponse>,
	IRequestHandler<CreateBudgetCommand, CreateBudgetCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IBudgetRepository _budgetRepository;
	private readonly ILogger<CreateBudgetHandler> _logger;

	public CreateBudgetHandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateBudgetHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_budgetRepository = _dataUnitOfWork.BudgetRepository;
		_logger = logger;
	}

	public Task<CreateBudgetQueryResponse> Handle(CreateBudgetQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<CreateBudgetCommandResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

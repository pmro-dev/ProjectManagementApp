using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Budgets.Show;

public class ShowBudgetHandler : IRequestHandler<ShowBudgetQuery, ShowBudgetQueryResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IBudgetRepository _budgetRepository;
    private readonly ILogger<ShowBudgetHandler> _logger;

	public ShowBudgetHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowBudgetHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_budgetRepository = _dataUnitOfWork.BudgetRepository;
		_logger = logger;
	}

	public Task<ShowBudgetQueryResponse> Handle(ShowBudgetQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

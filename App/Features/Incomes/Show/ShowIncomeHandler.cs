using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Incomes.Show;

public class ShowIncomeHandler :
	IRequestHandler<ShowIncomeQuery, ShowIncomeQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IIncomeRepository _incomeRepository;
	private readonly ILogger<ShowIncomeHandler> _logger;

	public ShowIncomeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowIncomeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_incomeRepository = _dataUnitOfWork.IncomeRepository;
		_logger = logger;
	}

	public Task<ShowIncomeQueryResponse> Handle(ShowIncomeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

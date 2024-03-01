using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Incomes.Delete;

public class DeleteIncomeHandler :
	IRequestHandler<DeleteIncomeQuery, DeleteIncomeQueryResponse>,
	IRequestHandler<DeleteIncomeCommand, DeleteIncomeCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IIncomeRepository _incomeRepository;
	private readonly ILogger<DeleteIncomeHandler> _logger;

	public DeleteIncomeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteIncomeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_incomeRepository = _dataUnitOfWork.IncomeRepository;
		_logger = logger;
	}

	public Task<DeleteIncomeQueryResponse> Handle(DeleteIncomeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteIncomeCommandResponse> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

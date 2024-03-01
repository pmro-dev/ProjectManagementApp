using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Incomes.Create;

public class CreateIncomeHandler :
	IRequestHandler<CreateIncomeQuery, CreateIncomeQueryResponse>,
	IRequestHandler<CreateIncomeCommand, CreateIncomeCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IIncomeRepository _incomeRepository;
	private readonly ILogger<CreateIncomeHandler> _logger;

	public CreateIncomeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateIncomeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_incomeRepository = _dataUnitOfWork.IncomeRepository;
		_logger = logger;
	}

	public Task<CreateIncomeQueryResponse> Handle(CreateIncomeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<CreateIncomeCommandResponse> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

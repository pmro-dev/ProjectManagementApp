using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Incomes.Edit;

public class EditIncomeHandler :
	IRequestHandler<EditIncomeQuery, EditIncomeQueryResponse>,
	IRequestHandler<EditIncomeCommand, EditIncomeCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IIncomeRepository _incomeRepository;
	private readonly ILogger<EditIncomeHandler> _logger;

	public EditIncomeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<EditIncomeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_incomeRepository = _dataUnitOfWork.IncomeRepository;
		_logger = logger;
	}

	public Task<EditIncomeQueryResponse> Handle(EditIncomeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<EditIncomeCommandResponse> Handle(EditIncomeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

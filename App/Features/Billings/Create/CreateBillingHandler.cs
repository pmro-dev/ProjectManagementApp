using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Billings.Create;

public class CreateBillingHandler :
	IRequestHandler<CreateBillingQuery, CreateBillingQueryResponse>,
	IRequestHandler<CreateBillingCommand, CreateBillingCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IBillingsRepository _billingsRepository;
	private readonly ILogger<CreateBillingHandler> _logger;

	public CreateBillingHandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateBillingHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_billingsRepository = _dataUnitOfWork.BillingsRepository;
		_logger = logger;
	}

	public Task<CreateBillingQueryResponse> Handle(CreateBillingQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<CreateBillingCommandResponse> Handle(CreateBillingCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

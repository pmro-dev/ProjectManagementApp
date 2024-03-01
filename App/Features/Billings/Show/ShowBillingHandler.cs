using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Billings.Show;

public class ShowBillingHandler :
	IRequestHandler<ShowBillingQuery, ShowBillingQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IBillingsRepository _billingsRepository;
	private readonly ILogger<ShowBillingHandler> _logger;

	public ShowBillingHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowBillingHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_billingsRepository = _dataUnitOfWork.BillingsRepository;
		_logger = logger;
	}

	public Task<ShowBillingQueryResponse> Handle(ShowBillingQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

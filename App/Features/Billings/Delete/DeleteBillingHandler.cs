using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Billings.Delete;

public class DeleteBillingHandler :
    IRequestHandler<DeleteBillingQuery, DeleteBillingQueryResponse>,
	IRequestHandler<DeleteBillingCommand, DeleteBillingCommandResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IBillingsRepository _billingsRepository;
    private readonly ILogger<DeleteBillingHandler> _logger;

    public DeleteBillingHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteBillingHandler> logger)
    {
        _dataUnitOfWork = dataUnitOfWork;
        _billingsRepository = _dataUnitOfWork.BillingsRepository;
        _logger = logger;
    }

	public Task<DeleteBillingQueryResponse> Handle(DeleteBillingQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteBillingCommandResponse> Handle(DeleteBillingCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Billings.Edit;

public class EditBillingHandler :
    IRequestHandler<EditBillingQuery, EditBillingQueryResponse>,
    IRequestHandler<EditBillingCommand, EditBillingCommandResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IBillingsRepository _billingsRepository;
    private readonly ILogger<EditBillingHandler> _logger;

    public EditBillingHandler(IDataUnitOfWork dataUnitOfWork, ILogger<EditBillingHandler> logger)
    {
        _dataUnitOfWork = dataUnitOfWork;
        _billingsRepository = _dataUnitOfWork.BillingsRepository;
        _logger = logger;
    }

    public Task<EditBillingQueryResponse> Handle(EditBillingQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EditBillingCommandResponse> Handle(EditBillingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using App.Common.ViewModels;
using MediatR;

namespace App.Features.Billings.Edit;

public class EditBillingQuery : IRequest<EditBillingQueryResponse>
{
	public Guid BillingId { get; set; }

	public EditBillingQuery(Guid billingId)
	{
		BillingId = billingId;
	}
}

public record EditBillingQueryResponse(
	WrapperViewModel<EditBillingInputVM, EditBillingOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}

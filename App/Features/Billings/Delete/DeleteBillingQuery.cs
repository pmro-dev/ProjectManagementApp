using MediatR;

namespace App.Features.Billings.Delete;

public class DeleteBillingQuery : IRequest<DeleteBillingQueryResponse>
{
	public Guid BillingId { get; set; }

	public DeleteBillingQuery(Guid billingId)
	{
		BillingId = billingId;
	}
}

public record DeleteBillingQueryResponse(
	DeleteBillingOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
using MediatR;

namespace App.Features.Billings.Show;

public class ShowBillingQuery : IRequest<ShowBillingQueryResponse>
{
	public Guid BillingId { get; set; }

	public ShowBillingQuery(Guid billingId)
	{
		BillingId = billingId;
	}
}

public record ShowBillingQueryResponse(
	ShowBillingOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
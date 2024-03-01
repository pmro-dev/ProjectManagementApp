using MediatR;

namespace App.Features.Billings.Delete;

public class DeleteBillingCommand : IRequest<DeleteBillingCommandResponse>
{
	public DeleteBillingInputVM InputVM { get; set; }

    public DeleteIncomeCommand(DeleteBillingInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record DeleteBillingCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}

using MediatR;

namespace App.Features.Billings.Edit;

public class EditBillingCommand : IRequest<EditBillingCommandResponse>
{
	public EditBillingInputVM InputVM { get; set; }

    public EditBillingCommand(EditBillingInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record EditBillingCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
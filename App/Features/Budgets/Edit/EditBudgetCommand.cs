using MediatR;

namespace App.Features.Budgets.Edit;

public class EditBudgetCommand : IRequest<EditBudgetCommandResponse>
{
	public EditBudgetInputVM InputVM { get; set; }

    public EditBudgetCommand(EditBudgetInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record EditBudgetCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
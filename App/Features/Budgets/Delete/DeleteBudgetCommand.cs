using MediatR;

namespace App.Features.Budgets.Delete;

public class DeleteBudgetCommand : IRequest<DeleteBudgetCommandResponse>
{
	public DeleteBudgetInputVM InputVM { get; set; }

    public DeleteBudgetCommand(DeleteBudgetInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record DeleteBudgetCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}

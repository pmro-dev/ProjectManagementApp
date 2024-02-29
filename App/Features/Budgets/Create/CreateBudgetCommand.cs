using MediatR;

namespace App.Features.Budgets.Create;

public class CreateBudgetCommand : IRequest<CreateBudgetCommandResponse>
{
	public Guid ProjectId { get; set; }
	public CreateBudgetInputVM InputVM { get; set; }

	public CreateBudgetCommand(Guid projectId, CreateBudgetInputVM inputVM)
	{
		ProjectId = projectId;
		InputVM = inputVM;
	}
}

public record CreateBudgetCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}

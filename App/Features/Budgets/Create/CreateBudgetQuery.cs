using App.Common.ViewModels;
using MediatR;

namespace App.Features.Budgets.Create;

public class CreateBudgetQuery : IRequest<CreateBudgetQueryResponse>
{
	public Guid ProjectId { get; set; }

	public CreateBudgetQuery(Guid projectId)
	{
		ProjectId = projectId;
	}
}

public record CreateBudgetQueryResponse(
	WrapperViewModel<CreateBudgetInputVM, CreateBudgetOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
using MediatR;

namespace App.Features.Incomes.Edit;

public class EditIncomeCommand : IRequest<EditIncomeCommandResponse>
{
	public EditIncomeInputVM InputVM { get; set; }

    public EditIncomeCommand(EditIncomeInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record EditIncomeCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
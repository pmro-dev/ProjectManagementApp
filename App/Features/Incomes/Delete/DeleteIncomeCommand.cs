using MediatR;

namespace App.Features.Incomes.Delete;

public class DeleteIncomeCommand : IRequest<DeleteIncomeCommandResponse>
{
	public DeleteIncomeInputVM InputVM { get; set; }

    public DeleteIncomeCommand(DeleteIncomeInputVM inputVM)
    {
		InputVM = inputVM;
	}
}

public record DeleteIncomeCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}

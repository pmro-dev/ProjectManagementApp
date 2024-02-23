using App.Features.Projects.Create.Models;
using MediatR;

namespace App.Features.Projects.Create;

public class CreateProjectCommand : IRequest<CreateProjectCommandResponse>
{
	public ProjectCreateInputVM InputVM { get; }

	public CreateProjectCommand(ProjectCreateInputVM inputVM)
	{
		InputVM = inputVM;
	}
}

public record CreateProjectCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
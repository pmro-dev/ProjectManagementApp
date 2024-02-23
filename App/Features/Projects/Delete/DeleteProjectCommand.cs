using MediatR;

namespace App.Features.Projects.Delete;

public class DeleteProjectCommand : IRequest<DeleteProjectCommandResponse>
{
    public Guid ProjectId { get; set; }

	public DeleteProjectCommand(Guid projectId)
	{
		ProjectId = projectId;
	}
}

public record DeleteProjectCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}

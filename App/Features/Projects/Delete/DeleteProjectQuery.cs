using App.Features.Projects.Delete.Models;
using MediatR;

namespace App.Features.Projects.Delete;

public class DeleteProjectQuery : IRequest<DeleteProjectQueryResponse>
{
    public Guid ProjectId { get; set; }

	public DeleteProjectQuery(Guid projectId)
	{
		ProjectId = projectId;
	}
}

public record DeleteProjectQueryResponse(
	ProjectDeleteOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
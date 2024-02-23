using App.Common.ViewModels;
using App.Features.Projects.Create.Models;
using MediatR;

namespace App.Features.Projects.Create;

public class CreateProjectQuery : IRequest<CreateProjectQueryResponse>
{
}


public record CreateProjectQueryResponse(
	WrapperViewModel<ProjectCreateInputVM, ProjectCreateOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
using App.Common.ViewModels;
using MediatR;

namespace App.Features.Teams.Common.Edit;

public class EditTeamSchemeQuery : IRequest<EditTeamSchemeQueryResponse>
{
	public Guid TeamId { get; set; }

	public EditTeamSchemeQuery(Guid teamId)
	{
		TeamId = teamId;
	}
}

public record EditTeamSchemeQueryResponse(
	WrapperViewModel<EditTeamInputVM, EditTeamOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
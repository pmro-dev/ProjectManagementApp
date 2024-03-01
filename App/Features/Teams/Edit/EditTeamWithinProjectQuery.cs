using App.Common.ViewModels;
using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamWithinProjectQuery : IRequest<EditTeamWithinProjectQueryResponse>
{
    public Guid TeamId { get; }
    public Guid ProjectId { get; }

    public EditTeamWithinProjectQuery(Guid projectId, Guid teamId)
    {
        ProjectId = projectId;
        TeamId = teamId;
    }
}

public record EditTeamWithinProjectQueryResponse(
    WrapperViewModel<EditTeamInputVM, EditTeamOutputVM>? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
)
{ }

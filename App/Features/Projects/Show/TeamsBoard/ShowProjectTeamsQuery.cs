using App.Features.Projects.Show.TeamsBoard.ViewModels;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Projects.Show.TeamsBoard;

public class ShowProjectTeamsQuery : IRequest<ShowProjectTeamsQueryResponse>
{
    public Guid ProjectId { get; set; }
    public int PageNumber { get; }
    public int ItemsPerPageCount { get; }
    public Expression<Func<TeamsBoardTeamVM, object>> OrderSelector { get; set; }

    public ShowProjectTeamsQuery(Guid projectId, Expression<Func<TeamsBoardTeamVM, object>> orderSelector, int pageNumber, int itemsPerPageCount)
    {
        ProjectId = projectId;
        PageNumber = pageNumber;
        ItemsPerPageCount = itemsPerPageCount;
        OrderSelector = orderSelector;
    }
}

public record ShowProjectTeamsQueryResponse(
    ProjectTeamsOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
)
{ }
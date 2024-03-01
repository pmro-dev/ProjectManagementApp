using App.Features.Teams.Common.Models;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Teams.Show;

public class ShowAllTeamSchemesQuery : IRequest<ShowAllTeamSchemesQueryResponse>
{
    public int PageNumber { get; }
    public int ItemsPerPageCount { get; }
    public Expression<Func<TeamModel, object>> OrderBySelector { get; }

    public ShowAllTeamSchemesQuery(int pageNumber, int itemsPerPageCount, Expression<Func<TeamModel, object>> orderBySelector)
    {
        PageNumber = pageNumber;
        ItemsPerPageCount = itemsPerPageCount;
        OrderBySelector = orderBySelector;
    }
}

public record ShowAllTeamSchemesQueryResponse(
    AllTeamSchemesOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
){}

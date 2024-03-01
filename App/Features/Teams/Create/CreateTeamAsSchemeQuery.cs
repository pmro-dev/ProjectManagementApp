using App.Common.ViewModels;
using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamAsSchemeQuery : IRequest<CreateTeamAsSchemeQueryResponse>
{
}

public record CreateTeamAsSchemeQueryResponse(
    WrapperViewModel<CreateTeamInputVM, CreateTeamOutputVM>? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
)
{ }
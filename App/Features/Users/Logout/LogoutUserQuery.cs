using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Logout;

public class LogoutUserQuery : IRequest<LogoutUserQueryResponse>
{
}

public record LogoutUserQueryResponse(IActionResult? Data = null, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }

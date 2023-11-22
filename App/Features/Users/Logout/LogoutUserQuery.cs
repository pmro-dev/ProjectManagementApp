using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Logout;

public class LogoutUserQuery : IRequest<IActionResult>
{
}

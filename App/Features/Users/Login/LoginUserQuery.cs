using App.Features.Users.Login.Models;
using MediatR;

namespace App.Features.Users.Login;

public class LoginUserQuery : IRequest<LoginUserQueryResponse>
{
	public LoginInputVM InputVM { get; }

	public LoginUserQuery(LoginInputVM inputVM)
	{
		InputVM = inputVM;
	}
}

public record LoginUserQueryResponse(string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }

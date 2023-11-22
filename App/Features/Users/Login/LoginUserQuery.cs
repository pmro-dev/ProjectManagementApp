using App.Features.Users.Login.Models;
using MediatR;

namespace App.Features.Users.Login;

public class LoginUserQuery : IRequest<bool>
{
	public LoginInputVM InputVM { get; }

	public LoginUserQuery(LoginInputVM inputVM)
	{
		InputVM = inputVM;
	}
}

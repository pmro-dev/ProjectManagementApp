using App.Features.Users.Register.Models;
using MediatR;

namespace App.Features.Users.Register;

public class RegisterUserCommand : IRequest<bool>
{
	public RegisterInputVM InputVM { get; }

	public RegisterUserCommand(RegisterInputVM inputVM)
	{
		InputVM = inputVM;
	}
}

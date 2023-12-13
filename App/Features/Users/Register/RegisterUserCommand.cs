using App.Features.Users.Register.Models;
using MediatR;

namespace App.Features.Users.Register;

public class RegisterUserCommand : IRequest<RegisterUserCommandResponse>
{
	public RegisterInputVM InputVM { get; }

	public RegisterUserCommand(RegisterInputVM inputVM)
	{
		InputVM = inputVM;
	}
}

public record RegisterUserCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}

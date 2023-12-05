using App.Features.Users.Common.Models;
using App.Features.Users.Common;
using MediatR;
using AutoMapper;
using App.Features.Users.Register.Interfaces;
using App.Common.Helpers;

namespace App.Features.Users.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
{
	private readonly IMapper _mapper;
	private readonly IUserRegisterService _userRegisterService;

	public RegisterUserHandler(IMapper mapper, IUserRegisterService userRegisterService)
	{
		_mapper = mapper;
		_userRegisterService = userRegisterService;
	}

	public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		UserDto userDto = _mapper.Map<UserDto>(request.InputVM);

		bool isDataInvalid = !UserDtoValidator.ValidData(userDto);

		if (isDataInvalid)
			return new RegisterUserCommandResponse(ErrorMessagesHelper.InvalidRegisterData, StatusCodesExtension.InvalidRegisterData);

		bool isUserRegistrationFailed = !await _userRegisterService.RegisterAsync(userDto);

		if (isUserRegistrationFailed)
			return new RegisterUserCommandResponse(ErrorMessagesHelper.RegistrationFailed, StatusCodesExtension.RegistrationFailed);
		
		return new RegisterUserCommandResponse();
	}
}

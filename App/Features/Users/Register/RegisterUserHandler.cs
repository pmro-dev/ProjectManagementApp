using App.Features.Users.Common.Models;
using App.Features.Users.Common;
using MediatR;
using AutoMapper;
using App.Features.Users.Register.Interfaces;

namespace App.Features.Users.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
{
	private readonly IMapper _mapper;
	private readonly IUserRegisterService _userRegisterService;

	public RegisterUserHandler(IMapper mapper, IUserRegisterService userRegisterService)
	{
		_mapper = mapper;
		_userRegisterService = userRegisterService;
	}

	public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		UserDto userDto = _mapper.Map<UserDto>(request.InputVM);

		bool isDataInvalid = !UserDtoValidator.ValidData(userDto);

		if (isDataInvalid)
		{
			return false;
			//ModelState.AddModelError(string.Empty, MessagesPacket.InvalidRegisterData);
			//return View();
		}

		////try
		////{
		bool isUserRegistrationFailed = !await _userRegisterService.RegisterAsync(userDto);

		if (isUserRegistrationFailed)
		{
			return false;
			//return View(UserCtrl.LoginAction);
		}

		////}
		////catch (Exception ex)
		////{
		//	_logger.LogCritical(ex, MessagesPacket.LogCreatingUserIdentityFailed, operationName);
		//	return Error();
		////}
		
		return true;
	}
}

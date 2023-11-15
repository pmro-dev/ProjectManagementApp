using App.Common.Helpers;
using App.Features.Users.Authentication;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles;
using App.Features.Users.Interfaces;
using App.Features.Users.Register.Interfaces;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using AutoMapper;

namespace App.Features.Users.Register;

public class UserRegisterService : IUserRegisterService
{
	private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IRoleRepository _roleRepository;
	private readonly IUserMapper _accountMapper;
	private readonly IMapper _mapper;
	private readonly ILogger<UserRegisterService> _logger;
	private readonly string _defaultRole = IdentitySeedData.DefaultRole;

	public UserRegisterService(IIdentityUnitOfWork identityUnitOfWork, ILogger<UserRegisterService> logger, IMapper mapper)
	{
		_identityUnitOfWork = identityUnitOfWork;
		_userRepository = _identityUnitOfWork.UserRepository;
		_roleRepository = _identityUnitOfWork.RoleRepository;
		_logger = logger;
		_accountMapper = accountMapper;
		_mapper = mapper;
	}

	public async Task<bool> RegisterAsync(IUserDto userDto)
	{
		bool isNotUsernameAvailableToRegister = !await CheckIsUsernameAvailable(userDto.Username);

		if (isNotUsernameAvailableToRegister) return false;

		userDto.Provider = AuthenticationConsts.DefaultScheme;
		await SetRoles(userDto);

		IUserModel userModel = _mapper.Map<IUserModel>(userDto);

		Task.WaitAny(Task.Run(async () =>
		{
			await _userRepository.AddAsync((UserModel)userModel);
			await _identityUnitOfWork.SaveChangesAsync();
		}));

		return true;
	}


	#region Local Methods

	private async Task<bool> CheckIsUsernameAvailable(string userName)
	{
		bool isNameTaken = await _userRepository.IsNameTakenAsync(userName);

		if (isNameTaken)
		{
			return false;
		}
		return true;
	}

	private async Task SetRoles(IUserDto userDto)
	{
		RoleModel? roleForNewUser = await _roleRepository.GetByFilterAsync(role => role.Name == _defaultRole);

		if (roleForNewUser is null)
		{
			_logger.LogCritical(MessagesPacket.LogCriticalErrorRoleForNewUserNotFoundInDb, nameof(SetRoles), nameof(roleForNewUser), _defaultRole);
			throw new InvalidOperationException(MessagesPacket.RoleForNewUserNotFoundInDb(nameof(roleForNewUser), _defaultRole));
		}

		userDto.UserRoles.Add(new UserRoleModel()
		{
			User = new UserModel { },
			UserId = userDto.UserId,
			Role = roleForNewUser,
			RoleId = roleForNewUser.Id
		});
	}

	#endregion
}

using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using App.Features.Users.Register.Interfaces;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Users.Register;

public class UserRegisterService : IUserRegisterService
{
	private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IRoleRepository _roleRepository;
	private readonly IUserFactory _userFactory;
	private readonly IMapper _mapper;
	private readonly ILogger<UserRegisterService> _logger;
	private readonly string _defaultRole = IdentityDbSeeder.DefaultRole;

	public UserRegisterService(IIdentityUnitOfWork identityUnitOfWork, ILogger<UserRegisterService> logger, IUserFactory userFactory, IMapper mapper)
	{
		_identityUnitOfWork = identityUnitOfWork;
		_userRepository = _identityUnitOfWork.UserRepository;
		_roleRepository = _identityUnitOfWork.RoleRepository;
		_logger = logger;
		_userFactory = userFactory;
		_mapper = mapper;
	}

	public async Task<bool> RegisterAsync(UserDto userDto)
	{
		bool isUsernameUnavailableToRegister = !await CheckIsUsernameAvailable(userDto.Username);

		if (isUsernameUnavailableToRegister) return false;

		userDto.Provider = AuthenticationConsts.DefaultScheme;
		await SetRoles(userDto);

		UserModel userModel = _mapper.Map<UserModel>(userDto);

		try
		{
			await _userRepository.AddAsync(userModel);
			await _identityUnitOfWork.SaveChangesAsync();
			return true;
		}
		catch(Exception ex)
		{
			_logger.LogError(ex, "Error occured while adding new user to DB");
			return false;
		}
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

	private async Task SetRoles(UserDto userDto)
	{
		RoleModel? roleForNewUser = await _roleRepository
			.GetByFilter(role => role.Name == _defaultRole)
			.SingleOrDefaultAsync();

		if (roleForNewUser is null)
		{
			_logger.LogCritical(ExceptionsMessages.LogCriticalErrorRoleNotFoundInDb, nameof(SetRoles), _defaultRole);
			throw new InvalidOperationException(ExceptionsMessages.RoleNotFoundInDb(nameof(SetRoles), _defaultRole));
		}

		var roleDto = _mapper.Map<RoleDto>(roleForNewUser);

		var userRoleDto = _userFactory.CreateUserRoleDto();
		userRoleDto.UserId = userDto.UserId;
		userRoleDto.RoleId = roleDto.Id;

		userDto.UserRoles.Add(userRoleDto);
	}

	#endregion
}

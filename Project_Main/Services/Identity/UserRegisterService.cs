using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Identity;
using Project_Main.Models.DataBases.Identity.DbSetup;
using Project_Main.Services.DTO;

namespace Project_Main.Services.Identity
{
	public class UserRegisterService : IUserRegisterService
	{
		private readonly IIdentityUnitOfWork _identityUnitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly ILogger<UserRegisterService> _logger;
		private readonly string _defaultRole = IdentitySeedData.DefaultRole;

		public UserRegisterService(IIdentityUnitOfWork identityUnitOfWork, ILogger<UserRegisterService> logger)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_userRepository = _identityUnitOfWork.UserRepository;
			_roleRepository = _identityUnitOfWork.RoleRepository;
			_logger = logger;
		}

        public async Task<bool> RegisterAsync(IUserDto userDto)
		{
			bool isNotUsernameAvailableToRegister = !await CheckIsUsernameAvailable(userDto.Username);

			if (isNotUsernameAvailableToRegister) return false;

			userDto.Provider = ConfigConstants.DefaultScheme;
			await SetRoles(userDto);

			UserModel userModel = AccountDtoService.TransferToUserModel(userDto);

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
				_logger.LogCritical(Messages.LogCriticalErrorRoleForNewUserNotFoundInDb, nameof(SetRoles), nameof(roleForNewUser), _defaultRole);
				throw new InvalidOperationException(Messages.RoleForNewUserNotFoundInDb(nameof(roleForNewUser), _defaultRole));
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
}

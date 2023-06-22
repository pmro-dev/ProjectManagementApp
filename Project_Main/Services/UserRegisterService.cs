using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Identity;
using Project_Main.Models.DataBases.Identity.DbSetup;

namespace Project_Main.Services
{
	public class UserRegisterService : IUserRegisterService
	{
		private readonly IIdentityUnitOfWork _identityUnitOfWork;
		private readonly IUserRepository _userRepository;

		public UserRegisterService(IIdentityUnitOfWork identityUnitOfWork)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_userRepository = _identityUnitOfWork.UserRepository;
		}

		public async Task<bool> IsPossibleToRegisterUserByProvidedData(string userName)
		{
			bool isNameTaken = await _userRepository.IsNameTakenAsync(userName);

			if (isNameTaken)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> RegisterUserAsync(string userName, string userPassword, string userEmail)
		{
			UserModel newUser = new()
			{
				Email = userEmail,
				FirstName = userName,
				Lastname = userName,
				Password = userPassword,
				Provider = ConfigConstants.DefaultScheme,
				Username = userName,
			};

			IRoleRepository roleRepository = _identityUnitOfWork.RoleRepository;
			RoleModel? roleForNewUser = await roleRepository.GetSingleByFilterAsync(role => role.Name == IdentitySeedData.DefaultRole);

			if (roleForNewUser is null)
			{
				_logger.LogCritical(Messages.LogCriticalErrorRoleForNewUserNotFoundInDb, nameof(RegisterUserAsync), nameof(roleForNewUser), IdentitySeedData.DefaultRole);
				throw new InvalidOperationException(Messages.RoleForNewUserNotFoundInDb(nameof(roleForNewUser), IdentitySeedData.DefaultRole));
			}

			newUser.NameIdentifier = newUser.UserId;
			newUser.UserRoles.Add(new UserRoleModel()
			{
				User = newUser,
				UserId = newUser.UserId,
				Role = roleForNewUser,
				RoleId = roleForNewUser.Id,
			});

			Task.WaitAny(Task.Run(async () =>
			{
				await _userRepository.AddAsync(newUser);
				await _identityUnitOfWork.SaveChangesAsync();
			}));

			return true;
		}
	}
}

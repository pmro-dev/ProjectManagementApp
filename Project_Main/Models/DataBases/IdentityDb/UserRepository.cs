using Microsoft.EntityFrameworkCore;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
	///<inheritdoc />
	public class UserRepository : GenericRepository<UserModel>, IUserRepository
	{
		private readonly CustomIdentityDbContext _identityContext;
		private readonly ILogger<UserRepository> _logger;
		private string operationName = string.Empty;
		private readonly string repoName = nameof(UserRepository);

		public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
		{
			_identityContext = identityContext;
			_logger = logger;
		}

		///<inheritdoc />
		public async Task<IUserModel?> GetWithDetailsAsync(string userId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), repoName);
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userId, nameof(userId), _logger);

			UserModel? userWithDetailsFromDb = await _identityContext
				.Set<UserModel>()
				.Where(user => user.UserId == userId)
				.Include(user => user.UserRoles)
				.SingleOrDefaultAsync();

			return userWithDetailsFromDb;
		}

		///<inheritdoc />
		public async Task<IUserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetByNameAndPasswordAsync), nameof(UserRepository));
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userLogin, nameof(userLogin), _logger);
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userPassword, nameof(userPassword), _logger);

			return await _identityContext
				.Set<UserModel>()
				.SingleOrDefaultAsync(user => user.Username == userLogin && user.Password == userPassword);
		}

		///<inheritdoc />
		public async Task<bool> IsNameTakenAsync(string userName)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(IsNameTakenAsync), repoName);
			bool result = await _identityContext
				.Set<UserModel>()
				.AnyAsync(user => user.Username == userName);

			return result;
		}

		///<inheritdoc />
		public async Task<ICollection<IRoleModel>> GetRolesAsync(string userId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetRolesAsync), repoName);
			ICollection<IRoleModel> userRoles = await _identityContext
				.Set<UserRoleModel>()
				.Where(user => user.UserId == userId)
				.Select(user => user.Role)
				.ToListAsync();

			return userRoles;
		}

	}
}

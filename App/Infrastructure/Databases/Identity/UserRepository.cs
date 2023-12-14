using App.Features.Exceptions.Throw;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Common;
using App.Infrastructure.Databases.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.Identity;

///<inheritdoc />
public class UserRepository : GenericRepository<UserModel>, IUserRepository
{
	private readonly CustomIdentityDbContext _identityContext;
	private readonly ILogger<UserRepository> _logger;

	public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
	{
		_identityContext = identityContext;
		_logger = logger;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetWithDetailsAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetWithDetailsAsync), userId, nameof(userId), _logger);

		UserModel? userWithDetailsFromDb = await _identityContext
			.Set<UserModel>()
			.Where(user => user.Id == userId)
			.Include(user => user.UserRoles)
			.SingleOrDefaultAsync();

		return userWithDetailsFromDb;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetByNameAndPasswordAsync), userLogin, nameof(userLogin), _logger);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetByNameAndPasswordAsync), userPassword, nameof(userPassword), _logger);

		Expression<Func<UserModel, bool>> userDataPredicateExpression = (UserModel user) => user.Username == userLogin && user.Password == userPassword;

		return await _identityContext
			.Set<UserModel>()
			.SingleOrDefaultAsync(userDataPredicateExpression);
	}

	///<inheritdoc />
	public async Task<bool> IsNameTakenAsync(string userName)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(IsNameTakenAsync), userName, nameof(userName), _logger);

		bool result = await _identityContext
			.Set<UserModel>()
			.AnyAsync(user => user.Username == userName);

		return result;
	}

	public async Task<bool> IsAccountExistedAsync(string userEmail)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(IsAccountExistedAsync), userEmail, nameof(userEmail), _logger);

		bool result = await _identityContext
			.Set<UserModel>()
			.AnyAsync(user => user.Email == userEmail);

		return result;
	}

	///<inheritdoc />
	public async Task<ICollection<RoleModel>> GetRolesAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetRolesAsync), userId, nameof(userId), _logger);

		ICollection<RoleModel> userRoles = await _identityContext
			.Set<UserRoleModel>()
			.Where(user => user.UserId == userId)
			.Select(user => user.Role)
			.ToListAsync();

		return userRoles;
	}

}

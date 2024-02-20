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
	private readonly ILogger<UserRepository> _logger;
	private readonly DbSet<UserModel> _dbSet;

	public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
	{
		_dbSet = identityContext.Set<UserModel>();
		_logger = logger;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetWithDetailsAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetWithDetailsAsync), userId, nameof(userId), _logger);

		UserModel? userWithDetailsFromDb = await _dbSet
			.Where(user => user.Id == userId)
			.Include(user => user.Roles)
			.SingleOrDefaultAsync();

		return userWithDetailsFromDb;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetByNameAndPasswordAsync), userLogin, nameof(userLogin), _logger);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetByNameAndPasswordAsync), userPassword, nameof(userPassword), _logger);

		Expression<Func<UserModel, bool>> userDataPredicateExpression = (UserModel user) => user.Username == userLogin && user.Password == userPassword;

		return await _dbSet.SingleOrDefaultAsync(userDataPredicateExpression);
	}

	///<inheritdoc />
	public async Task<bool> IsNameTakenAsync(string userName)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(IsNameTakenAsync), userName, nameof(userName), _logger);

		bool result = await _dbSet
			.AnyAsync(user => user.Username == userName);

		return result;
	}

	public async Task<bool> DoesAccountExistAsync(string userEmail)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(DoesAccountExistAsync), userEmail, nameof(userEmail), _logger);

		bool result = await _dbSet
			.AnyAsync(user => user.Email == userEmail);

		return result;
	}

	//TODO CHECK THIS change
	///<inheritdoc />
	public async Task<ICollection<RoleModel>> GetRolesAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetRolesAsync), userId, nameof(userId), _logger);

		ICollection<RoleModel>? userRoles = await _dbSet
			.Where(user => user.Id == userId).Include(user => user.Roles).Select(u => u.Roles).SingleOrDefaultAsync();

		return userRoles ?? new List<RoleModel>();
	}
}

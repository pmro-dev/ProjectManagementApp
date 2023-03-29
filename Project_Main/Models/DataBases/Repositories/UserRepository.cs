using Microsoft.EntityFrameworkCore;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Models.DataBases.Repositories
{
	public class UserRepository : GenericRepository<UserModel>, IUserRepository
	{
		private readonly CustomDbContext _dbContext;
		private readonly ILogger<UserRepository> _logger;
		private string operationName = string.Empty;

		public UserRepository(CustomDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<UserModel?> GetByNameAndPasswordAsync(string login, string password)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetByNameAndPasswordAsync), nameof(UserRepository));
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref login, nameof(login), _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref password, nameof(password), _logger);

			return await _dbContext.Set<UserModel>().SingleAsync(user => user.NameIdentifier == login && user.Password == password);
		}

		public async Task<UserModel> GetWithDetailsAsync(string id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), nameof(UserRepository));
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref id, nameof(id), _logger);

			return await _dbContext.Set<UserModel>().Where(u => u.UserId == id).Include(u => u.UserRoles).SingleAsync();
		}

		public async Task<bool> IsNameTakenAsync(string name)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(IsNameTakenAsync), nameof(UserRepository));
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref name, nameof(name), _logger);

			return await _dbContext.Set<UserModel>().AnyAsync(u => u.NameIdentifier == name);
		}
	}
}

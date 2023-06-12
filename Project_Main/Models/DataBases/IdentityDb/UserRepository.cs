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
		public async Task<UserModel?> GetWithDetailsAsync(string userId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), repoName);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userId, nameof(userId), _logger);

            UserModel? userWithDetailsFromDb = await _identityContext.Set<UserModel>().Where(user => user.UserId == userId).Include(user => user.UserRoles).SingleOrDefaultAsync();
            //HelperCheck.IfInstanceNullThrowException(operationName, userWithDetailsFromDb, nameof(userWithDetailsFromDb), _logger);

            return userWithDetailsFromDb;
        }

		///<inheritdoc />
		public async Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetByNameAndPasswordAsync), nameof(UserRepository));
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userLogin, nameof(userLogin), _logger);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userPassword, nameof(userPassword), _logger);

            return await _identityContext.Set<UserModel>().SingleOrDefaultAsync(user => user.Username == userLogin && user.Password == userPassword);
        }

		///<inheritdoc />
		public async Task<bool> IsNameTakenAsync(string userName)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(IsNameTakenAsync), repoName);
            bool result = false;

            result = await _identityContext.Set<UserModel>().AnyAsync(user => user.Username == userName);
            //HelperCheck.IfInstanceNullThrowException(operationName, userFromDb, nameof(userFromDb), _logger);

            return result;
        }

		///<inheritdoc />
		public async Task<IEnumerable<RoleModel>> GetRolesAsync(string userId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetRolesAsync), repoName);
            List<RoleModel> userRoles = await _identityContext.Set<UserRoleModel>()
            .Where(user => user.UserId == userId)
            .Select(user => user.Role)
            .ToListAsync();

            return userRoles;
        }


        //TODO implement Update feature
        //public async Task<bool> UpdateUserAsync(UserModel userToUpdate)
        //{
        //	operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(UpdateUserAsync), repoName);
        //	HelperCheck.IfArgumentNullThrowException(operationName, userToUpdate, nameof(userToUpdate), _logger);

        //	TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
        //	{
        //		UserModel? userToUpdateFromDb = await _identityContext.GetUserWithDetailsAsync(userToUpdate.UserId);
        //		//HelperCheck.IfInstanceNullThrowException(operationName, userToUpdateFromDb, nameof(userToUpdateFromDb), _logger);

        //		userToUpdateFromDb.Username = userToUpdate.Username;
        //		userToUpdateFromDb.Password = userToUpdate.Password;
        //		userToUpdateFromDb.Email = userToUpdate.Email;
        //		userToUpdateFromDb.FirstName = userToUpdate.FirstName;
        //		userToUpdateFromDb.Lastname = userToUpdate.Lastname;
        //		userToUpdateFromDb.Provider = userToUpdate.Provider;
        //		userToUpdateFromDb.NameIdentifier = userToUpdate.NameIdentifier;

        //		if (!userToUpdate.UserRoles.Any())
        //		{
        //			userToUpdateFromDb.UserRoles = userToUpdate.UserRoles;
        //		}

        //		await _identityContext.UpdateUserAsync(userToUpdateFromDb);
        //	});

        //	await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
        //	return true;
        //}
    }
}

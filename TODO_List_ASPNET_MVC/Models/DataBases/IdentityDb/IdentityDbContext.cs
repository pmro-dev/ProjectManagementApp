using Microsoft.EntityFrameworkCore;
using Identity_Domain_Entities;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBases.Common.Helpers;

namespace TODO_List_ASPNET_MVC.Models.DataBases.IdentityDb
{
	/// <summary>
	/// </summary>
	public class IdentityDbContext : DbContext
	{
		private const int IntValueForSuccessOperation = 1;
		private readonly ILogger<IdentityDbContext> _logger;
		private string operationName = string.Empty;

		public virtual DbSet<UserModel> Users => Set<UserModel>();
		public virtual DbSet<RoleModel> Roles => Set<RoleModel>();
		public virtual DbSet<UserRoleModel> UserRoles => Set<UserRoleModel>();

		public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ILogger<IdentityDbContext> logger) : base(options)
		{
			_logger = logger;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserRoleModel>().HasKey(ur => new { ur.UserId, ur.RoleId });

			_logger.LogInformation(Messages.BuildingSucceedLogger, nameof(OnModelCreating), nameof(IdentityDbContext));
		}

		public async Task<int> AddUserAsync(UserModel newUser)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(AddUserAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, newUser, nameof(newUser), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);

			await Users.AddAsync(newUser);
			return await SaveChangesAsync();
		}

		public async Task<UserModel> GetUserWithDetailsAsync(string userId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserWithDetailsAsync), nameof(IdentityDbContext));

			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userId, operationName, _logger);

			return await Users.Where(u => u.UserId == userId).Include(u => u.UserRoles).SingleAsync();
		}

		public async Task<UserModel> GetUserAsync(string userId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserAsync), nameof(IdentityDbContext));

			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId, operationName, _logger);

			return await Users.SingleAsync(u => u.UserId == userId);
		}

		public async Task<UserModel> GetUserByNameAndPasswordAsync(string userName, string userPassword)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserByNameAndPasswordAsync), nameof(IdentityDbContext));

			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId, operationName, _logger);

			return await Users.SingleAsync(u => u.Username == userName && u.Password == userPassword);
		}

		public async Task<int> UpdateUserAsync(UserModel userToUpdate)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateUserAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, userToUpdate, nameof(userToUpdate), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(Roles, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userToUpdate.UserId, operationName, _logger);

			Users.Update(userToUpdate);
			return await SaveChangesAsync();
		}

		public async Task<int> DeleteUserAsync(UserModel userToDelete)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteUserAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, userToDelete, nameof(userToDelete), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Users, _logger, operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(UserRoles, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userToDelete.UserId, operationName, _logger);

			UserModel userFromDbToDelete = await Users.SingleAsync(u => u.UserId == userToDelete.UserId);
			List<UserRoleModel> userRoles = await UserRoles.Where(u => u.UserId == userToDelete.UserId).ToListAsync();

			using var transaction = Database.BeginTransaction();

			try
			{
				UserRoles.RemoveRange(userRoles);
				Users.Remove(userFromDbToDelete);
				await SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, Messages.TransactionFailedLogger, operationName, userToDelete.UserId);
				throw;
			}

			return IntValueForSuccessOperation;
		}

		public async Task<bool> DoesUserExist(string nameIdentifier)
		{
			return await Users.AnyAsync(u => u.NameIdentifier == nameIdentifier);
		}

		// ///////////////////////////

		public async Task<int> AddRoleAsync(RoleModel newRole)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(AddRoleAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, newRole, nameof(newRole), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Roles, _logger, operationName);

			await Roles.AddAsync(newRole);
			return await SaveChangesAsync();
		}

		public async Task<RoleModel> GetRoleAsync(string roleId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetRoleAsync), nameof(IdentityDbContext));

			DbContextValidators.CheckDbSetIfNullThrowException(Roles, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userId, operationName, _logger);

			return await Roles.SingleAsync(r => r.Id == roleId);
		}

		public async Task<int> UpdateRoleAsync(RoleModel roleToUpdate)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateRoleAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, roleToUpdate, nameof(roleToUpdate), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Roles, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userToUpdate.UserId, operationName, _logger);

			Roles.Update(roleToUpdate);
			return await SaveChangesAsync();
		}

		public async Task<int> DeleteRoleAsync(RoleModel roleToDelete)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteRoleAsync), nameof(IdentityDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, roleToDelete, nameof(roleToDelete), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Roles, _logger, operationName);
			// First I have change Ids type from int to Guid in AppDbContext to use this method ->
			//await DbContextValidators.CheckItemExistsIfNotThrowException(Users, nameof(Users), userToDelete.UserId, operationName, _logger);

			RoleModel roleFromDbToDelete = await Roles.SingleAsync(r => r.Id == roleToDelete.Id);

			Roles.Remove(roleFromDbToDelete);
			return await SaveChangesAsync();
		}

		public async Task<List<string>> GetRolesForUserAsync(string userId)
		{
			return await UserRoles.Where(ur => ur.UserId == userId).Include(r => r.Role).Select(ur => ur.Role.Name).ToListAsync();
		}
	}
}

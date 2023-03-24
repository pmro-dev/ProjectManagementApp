using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TODO_Domain_Entities;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBase.Abstraction;
using static TODO_Domain_Entities.Helpers.TaskStatusHelper;

namespace TODO_List_ASPNET_MVC.Models.DataBase
{
	/// <summary>
	/// Context class that implements DbContext.
	/// </summary>
	public class AppDbContext : DbContext, IAppDbContext
	{
		private readonly ILogger<AppDbContext> _logger;
		private string operationName = string.Empty;
		private const int intValueForSuccessOperation = 1;

		public virtual DbSet<TaskModel> Tasks => Set<TaskModel>();
		public virtual DbSet<TodoListModel> TodoLists => Set<TodoListModel>();
        public virtual DbSet<TaskTagModel> TaskTags => Set<TaskTagModel>();

        /// <summary>
        /// Initilizes object and Ensures that database is created.
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
		{
			_logger = logger;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var todoItemBuilder = modelBuilder.Entity<TaskModel>();

			todoItemBuilder.Property(x => x.Status)
				.HasConversion(new EnumToStringConverter<TaskStatusType>());

            modelBuilder.Entity<TaskTagModel>().HasKey(tt => new { tt.TaskId, tt.TagId });

            _logger.LogInformation(Messages.BuildingSuccedLogger, nameof(OnModelCreating), nameof(AppDbContext));
		}

		/// <inheritdoc/>
		public async Task<int> AddTodoListAsync(TodoListModel newTodoList)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(AddTodoListAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, newTodoList, nameof(newTodoList), _logger);
			CheckDbSetIfNullThrowException(TodoLists);

			await TodoLists.AddAsync(newTodoList);
			return await SaveChangesAsync();
		}

		/// <inheritdoc/>
		public async Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetTodoListWithDetailsAsync), nameof(AppDbContext));

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);
			CheckDbSetIfNullThrowException(TodoLists);
			await CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId);
			return await TodoLists.Where(l => l.Id == todoListId && l.UserId == signedInUserId).Include(l => l.Tasks).SingleAsync();
		}

		/// <inheritdoc/>
		public async Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetTodoListAsync), nameof(AppDbContext));

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);
			CheckDbSetIfNullThrowException(TodoLists);
			await CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId);
			
			return await TodoLists.SingleAsync(l => l.Id == todoListId && l.UserId == signedInUserId);
		}

		/// <inheritdoc/>
		public async Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsAsync), nameof(AppDbContext));
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);
			CheckDbSetIfNullThrowException(TodoLists);

			return await TodoLists.Where(l => l.UserId == signedInUserId).ToListAsync();
		}

		/// <inheritdoc/>
		public async Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsWithDetailsAsync), nameof(AppDbContext));
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);
			CheckDbSetIfNullThrowException(TodoLists);
			CheckDbSetIfNullThrowException(Tasks);

			return await TodoLists.Where(l => l.UserId == signedInUserId).Include(l => l.Tasks).ToListAsync();
		}

		/// <inheritdoc/>
		public async Task<int> UpdateTodoListAsync(TodoListModel todoListToUpdate)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(UpdateTodoListAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, todoListToUpdate, nameof(todoListToUpdate), _logger);
			CheckDbSetIfNullThrowException(TodoLists);
			CheckDbSetIfNullThrowException(Tasks);
			await CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListToUpdate.Id);

			TodoLists.Update(todoListToUpdate);
			return await SaveChangesAsync();
		}

		/// <inheritdoc/>
		public async Task<int> DeleteTodoListAsync(TodoListModel todoListToDelete)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTodoListAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, todoListToDelete, nameof(todoListToDelete), _logger);
			CheckDbSetIfNullThrowException(TodoLists);
			CheckDbSetIfNullThrowException(Tasks);
			await CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListToDelete.Id);

			var tasksToDelete = await Tasks.Where(t => t.TodoListId == todoListToDelete.Id).ToListAsync();

			using var transaction = this.Database.BeginTransaction();

			try
			{
				Tasks.RemoveRange(tasksToDelete);
				TodoLists.Remove(todoListToDelete);
				await SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, Messages.TransactionFailedLogger, operationName, todoListToDelete.Id);
				throw;
			}

			return intValueForSuccessOperation;
		}

		/// <inheritdoc/>
		public async Task<int> CreateTaskAsync(TaskModel newTask)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(CreateTaskAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, newTask, nameof(newTask), _logger);
			CheckDbSetIfNullThrowException(Tasks);

			Tasks.Add(newTask);
			return await SaveChangesAsync();
		}

		/// <inheritdoc/>
		public async Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(ReadTaskAsync), nameof(AppDbContext));

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);
			CheckDbSetIfNullThrowException(Tasks);
			await CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskId);

			var taskFromDb = await Tasks.SingleAsync(x => x.Id == taskId && x.UserId == signedInUserId);
			return taskFromDb;
		}

		/// <inheritdoc/>
		public async Task<int> UpdateTaskAsync(TaskModel taskToUpdate)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(UpdateTaskAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, taskToUpdate, nameof(taskToUpdate), _logger);
			CheckDbSetIfNullThrowException(Tasks);
			await CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskToUpdate.Id);

			Tasks.Update(taskToUpdate);
			return await SaveChangesAsync();
		}

		/// <inheritdoc/>
		public async Task<int> DeleteTaskAsync(TaskModel taskToDelete)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

			HelperCheck.IfArgumentModelNullThrowException(operationName, taskToDelete, nameof(taskToDelete), _logger);
			CheckDbSetIfNullThrowException(Tasks);
			await CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskToDelete.Id);

			Tasks.Remove(taskToDelete);
			return await SaveChangesAsync();
		}

		public async Task<bool> DoesTodoListExistByName(string todoListName)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

			if (string.IsNullOrEmpty(todoListName))
			{
				_logger.LogError(Messages.ParamObjectNullLogger, operationName, nameof(todoListName));
				throw new ArgumentNullException(nameof(todoListName), Messages.ParamObjectNull(operationName, nameof(todoListName)));
			}

			return await TodoLists.AnyAsync(t => t.Name == todoListName);
		}

		/// <inheritdoc/>
		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		public void CheckAllDbSetsIfAnyNullThrowException(string operationName)
		{
			this.operationName = operationName;
			CheckDbSetIfNullThrowException(TodoLists);
			CheckDbSetIfNullThrowException(Tasks);
		}

		private void CheckDbSetIfNullThrowException<T>(DbSet<T> dbSet) where T : class
		{
			if (dbSet == null)
			{
				_logger.LogCritical(Messages.ParamObjectNullLogger, operationName, typeof(T).Name);
				throw new InvalidOperationException(Messages.DbSetNullEx);
			}
		}

		private async Task CheckItemExistsIfNotThrowException<T>(DbSet<T> dbSet, string dbSetName, int id) where T : BasicModelAbstract
		{
			if (await dbSet.AnyAsync(x => x.Id == id) is false)
			{
				_logger.LogInformation(Messages.EntityNotFoundInDbSetLogger, operationName, dbSetName, id);
				throw new InvalidOperationException(Messages.ItemNotFoundInDb);
			}
		}
	}
}

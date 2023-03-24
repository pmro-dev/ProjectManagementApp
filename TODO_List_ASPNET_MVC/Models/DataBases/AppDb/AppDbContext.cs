using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TODO_Domain_Entities;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBases.Common.Helpers;
using static TODO_Domain_Entities.Helpers.TaskStatusHelper;

namespace TODO_List_ASPNET_MVC.Models.DataBases.AppDb
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

            _logger.LogInformation(Messages.BuildingSucceedLogger, nameof(OnModelCreating), nameof(AppDbContext));
        }

        /// <inheritdoc/>
        public async Task<int> AddTodoListAsync(TodoListModel newTodoList)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(AddTodoListAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, newTodoList, nameof(newTodoList), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);

			await TodoLists.AddAsync(newTodoList);
            return await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetTodoListWithDetailsAsync), nameof(AppDbContext));

            HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId, operationName, _logger);

            return await TodoLists.Where(l => l.Id == todoListId && l.UserId == signedInUserId).Include(l => l.Tasks).SingleAsync();
        }

        /// <inheritdoc/>
        public async Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetTodoListAsync), nameof(AppDbContext));

            HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListId, operationName, _logger);

            return await TodoLists.SingleAsync(l => l.Id == todoListId && l.UserId == signedInUserId);
        }

        /// <inheritdoc/>
        public async Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsAsync), nameof(AppDbContext));
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);

			return await TodoLists.Where(l => l.UserId == signedInUserId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsWithDetailsAsync), nameof(AppDbContext));
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);

            return await TodoLists.Where(l => l.UserId == signedInUserId).Include(l => l.Tasks).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateTodoListAsync(TodoListModel todoListToUpdate)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateTodoListAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, todoListToUpdate, nameof(todoListToUpdate), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListToUpdate.Id, operationName, _logger);

			TodoLists.Update(todoListToUpdate);
            return await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteTodoListAsync(TodoListModel todoListToDelete)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTodoListAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, todoListToDelete, nameof(todoListToDelete), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(TodoLists, nameof(TodoLists), todoListToDelete.Id, operationName, _logger);
            
            var tasksToDelete = await Tasks.Where(t => t.TodoListId == todoListToDelete.Id).ToListAsync();

            using var transaction = Database.BeginTransaction();

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
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(CreateTaskAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, newTask, nameof(newTask), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);

			Tasks.Add(newTask);
            return await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(ReadTaskAsync), nameof(AppDbContext));

            HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskId, operationName, _logger);

            var taskFromDb = await Tasks.SingleAsync(x => x.Id == taskId && x.UserId == signedInUserId);
            return taskFromDb;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateTaskAsync(TaskModel taskToUpdate)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateTaskAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, taskToUpdate, nameof(taskToUpdate), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskToUpdate.Id, operationName, _logger);

            Tasks.Update(taskToUpdate);
            return await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteTaskAsync(TaskModel taskToDelete)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

            HelperCheck.IfArgumentModelNullThrowException(operationName, taskToDelete, nameof(taskToDelete), _logger);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
			await DbContextValidators.CheckItemExistsIfNotThrowException(Tasks, nameof(Tasks), taskToDelete.Id, operationName, _logger);

            Tasks.Remove(taskToDelete);
            return await SaveChangesAsync();
        }

        public async Task<bool> DoesTodoListExistByName(string todoListName)
        {
            operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

            if (string.IsNullOrEmpty(todoListName))
            {
                _logger.LogError(Messages.ParamNullOrEmptyLogger, operationName, nameof(todoListName));
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
			DbContextValidators.CheckDbSetIfNullThrowException(TodoLists, _logger, this.operationName);
			DbContextValidators.CheckDbSetIfNullThrowException(Tasks, _logger, this.operationName);
        }
    }
}

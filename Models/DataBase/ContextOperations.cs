using EntityFramework.Exceptions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TODO_Domain_Entities;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBase.Abstraction;

namespace TODO_List_ASPNET_MVC.Models.DataBase
{
	/// <inheritdoc/>
	public class ContextOperations : IContextOperations
	{
		private readonly IAppDbContext _appDbContext;
		private readonly ILogger<ContextOperations> _logger;
		delegate Task TryCatchBlockDelegateType();
		private string methodName = string.Empty;
		private string operationName = string.Empty;
		private readonly string contextName = nameof(ContextOperations);

		/// <summary>
		/// Initializes <see cref="IAppDbContext"/> object for Database Operations.
		/// </summary>
		/// <param name="appDbContext">It is a <see cref="IAppDbContext"/> field that is base context for DataBase.</param>
		/// <param name="_logger">It is a <see cref="ILogger"/> field.</param>
		public ContextOperations(IAppDbContext appDbContext, ILogger<ContextOperations> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> AddTodoListAsync(TodoListModel todoList)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(AddTodoListAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, todoList, nameof(todoList), _logger);

			TryCatchBlockDelegateType operationsOnDbContextDelegate = new(async () =>
			{
				await _appDbContext.AddTodoListAsync(todoList);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsOnDbContextDelegate);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> DeleteTodoListAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTodoListAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				TodoListModel? todoListToDelete = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListToDelete, nameof(todoListToDelete), _logger);

				await _appDbContext.DeleteTodoListAsync(todoListToDelete);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsAsync), contextName);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			List<TodoListModel> lists = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				lists = await _appDbContext.GetAllTodoListsAsync(signedInUserId) ?? new List<TodoListModel>();
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return lists;
		}

		/// <inheritdoc/>
		public virtual async Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsWithDetailsAsync), contextName);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			List<TodoListModel> lists = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				lists = await _appDbContext.GetAllTodoListsWithDetailsAsync(signedInUserId) ?? new List<TodoListModel>();
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return lists;
		}

		/// <inheritdoc/>
		public virtual async Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetTodoListWithDetailsAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TodoListModel todoListFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				todoListFromDb = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListFromDb, nameof(todoListFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);

			return todoListFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(GetTodoListAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TodoListModel todoListFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				todoListFromDb = await _appDbContext.GetTodoListAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListFromDb, nameof(todoListFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return todoListFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> UpdateTodoListAsync(TodoListModel todoList)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(UpdateTodoListAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, todoList, nameof(todoList), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				TodoListModel? todoListToUpdate = await GetTodoListWithDetailsAsync(todoList.Id, todoList.UserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListToUpdate, nameof(todoListToUpdate), _logger);

				todoListToUpdate.Name = todoList.Name;
				todoListToUpdate.Tasks = todoList.Tasks;

				await _appDbContext.UpdateTodoListAsync(todoListToUpdate);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		/// <inheritdoc/>
		public async Task<bool> DuplicateTodoListWithDetailsAsync(int todoListId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DuplicateTodoListWithDetailsAsync), contextName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				TodoListModel? todoListFromDb = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListFromDb, nameof(todoListFromDb), _logger);

				List<TaskModel> tasksFromDb = await GetTasksForTodoList(todoListId);

				StringBuilder nameBuilder = new(todoListFromDb.Name);
				nameBuilder.Append(" - Copy");
				int iterator = 2;

				while (await _appDbContext.TodoLists.AnyAsync(l => l.Name == nameBuilder.ToString()))
				{
					nameBuilder.Append($" {iterator++}");
				}

				var newTodoList = new TodoListModel()
				{
					Id = 0,
					Name = nameBuilder.ToString(),
					Tasks = tasksFromDb.Select(t =>
					{
						t.Id = 0;
						t.CreationDate = DateTime.Now;
						t.LastModificationDate = DateTime.Now;
						t.UserId = signedInUserId;
						return t;
					}).ToList(),
					UserId = signedInUserId
				};

				await _appDbContext.AddTodoListAsync(newTodoList);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> CreateTaskAsync(TaskModel newTask)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(CreateTaskAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, newTask, nameof(newTask), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				await _appDbContext.CreateTaskAsync(newTask);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(ReadTaskAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TaskModel taskFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				taskFromDb = await _appDbContext.ReadTaskAsync(taskId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, taskFromDb, nameof(taskFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return taskFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> UpdateTaskAsync(TaskModel taskToUpdate)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(UpdateTaskAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, taskToUpdate, nameof(taskToUpdate), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				TaskModel? tempTaskUpdate = await ReadTaskAsync(taskToUpdate.Id, taskToUpdate.UserId);
				HelperCheck.IfInstanceNullThrowException(operationName, tempTaskUpdate, nameof(tempTaskUpdate), _logger);

				tempTaskUpdate.Id = taskToUpdate.Id;
				tempTaskUpdate.Title = taskToUpdate.Title;
				tempTaskUpdate.Description = taskToUpdate.Description;
				tempTaskUpdate.DueDate = taskToUpdate.DueDate;
				tempTaskUpdate.LastModificationDate = taskToUpdate.LastModificationDate;
				tempTaskUpdate.ReminderDate = taskToUpdate.ReminderDate;
				tempTaskUpdate.Status = taskToUpdate.Status;
				tempTaskUpdate.TodoListId = taskToUpdate.TodoListId;
				await _appDbContext.UpdateTaskAsync(tempTaskUpdate);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> DeleteTaskAsync(int taskId, string signedInUserId)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.IFUserIdNullThrowException(operationName, ref signedInUserId, _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlock = new(async () =>
			{
				TaskModel? taskToDelete = await ReadTaskAsync(taskId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, taskToDelete, nameof(taskToDelete), _logger);
				await _appDbContext.DeleteTaskAsync(taskToDelete);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(operationsForDbTryCatchBlock);
			return true;
		}

		public virtual async Task<bool> DoesTodoListWithSameNameExist(string todoListName)
		{
			operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

			if (string.IsNullOrEmpty(todoListName))
			{
				_logger.LogError(Messages.ParamObjectNullLogger, operationName, nameof(todoListName));
				throw new ArgumentNullException(nameof(todoListName), Messages.ParamObjectNull(operationName, nameof(todoListName)));
			}

			return await _appDbContext.DoesTodoListExistByName(todoListName);
		}

		private async Task<List<TaskModel>> GetTasksForTodoList(int todoListId)
		{
			return await _appDbContext.Tasks.AsNoTracking().Where(t => t.TodoListId == todoListId).ToListAsync();
		}

		private async Task ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptions(TryCatchBlockDelegateType operationsForDbTryCatchBlock)
		{
			methodName = operationsForDbTryCatchBlock.Method.Name;

			try
			{
				await operationsForDbTryCatchBlock.Invoke();
			}
			catch (CannotInsertNullException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (NumericOverflowException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (ReferenceConstraintException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (MaxLengthExceededException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (UniqueConstraintException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (SqlException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (InvalidOperationException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (AggregateException agg)
			{
				_logger.LogError(agg.Flatten().InnerException, Messages.ErrorOnMethodLogger, methodName);
				throw;
			}
		}

		private void LogError(Exception ex, string methodName)
		{
			_logger.LogError(ex, Messages.ExceptionOccuredLogger, ex.GetType(), methodName);
		}
	}
}

using EntityFramework.Exceptions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using TODO_Domain_Entities;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBases.AppDb;

namespace TODO_List_ASPNET_MVC.Models.DataBases.Repositories
{
	/// <inheritdoc/>
	public class ContextOperations : IContextOperations
	{
		private readonly IAppDbContext _appDbContext;
		private readonly ILogger<ContextOperations> _logger;
		delegate Task TryCatchBlockForDbDelType();
		private string methodName = string.Empty;
		private string operationName = string.Empty;
		private readonly string contextName = nameof(ContextOperations);
		private readonly int startingValueForDuplication = 2;
		private readonly int ValueIndicatesEmptySet = 0;

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
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(AddTodoListAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, todoList, nameof(todoList), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				await _appDbContext.AddTodoListAsync(todoList);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> DeleteTodoListAsync(int todoListId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTodoListAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				TodoListModel? todoListToDelete = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListToDelete, nameof(todoListToDelete), _logger);

				await _appDbContext.DeleteTodoListAsync(todoListToDelete);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsAsync), contextName);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			List<TodoListModel> lists = new();

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				lists = await _appDbContext.GetAllTodoListsAsync(signedInUserId) ?? new List<TodoListModel>();
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return lists;
		}

		/// <inheritdoc/>
		public virtual async Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetAllTodoListsWithDetailsAsync), contextName);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			List<TodoListModel> lists = new();

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				lists = await _appDbContext.GetAllTodoListsWithDetailsAsync(signedInUserId) ?? new List<TodoListModel>();
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return lists;
		}

		/// <inheritdoc/>
		public virtual async Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetTodoListWithDetailsAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TodoListModel todoListFromDb = new();

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				todoListFromDb = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListFromDb, nameof(todoListFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);

			return todoListFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetTodoListAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TodoListModel todoListFromDb = new();

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				todoListFromDb = await _appDbContext.GetTodoListAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListFromDb, nameof(todoListFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return todoListFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> UpdateTodoListAsync(TodoListModel todoList)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateTodoListAsync), contextName);
			HelperCheck.IfArgumentModelNullThrowException(operationName, todoList, nameof(todoList), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				TodoListModel? todoListToUpdate = await GetTodoListWithDetailsAsync(todoList.Id, todoList.UserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListToUpdate, nameof(todoListToUpdate), _logger);

				todoListToUpdate.Name = todoList.Name;

				if (todoList.Tasks.Count != ValueIndicatesEmptySet)
				{
					todoListToUpdate.Tasks = todoList.Tasks;
				}

				await _appDbContext.UpdateTodoListAsync(todoListToUpdate);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public async Task<bool> DuplicateTodoListWithDetailsAsync(int todoListId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DuplicateTodoListWithDetailsAsync), contextName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				TodoListModel? todoListWithDetails = await _appDbContext.GetTodoListWithDetailsAsync(todoListId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, todoListWithDetails, nameof(todoListWithDetails), _logger);

				TodoListModel newTodoList = new()
				{
					Id = 0,
					Name = await ProvideNameForDuplicateToDoList(todoListWithDetails.Name, signedInUserId),
					Tasks = todoListWithDetails.Tasks.Select(t => new TaskModel()
					{
						Id = 0,
						CreationDate = DateTime.Now,
						LastModificationDate = DateTime.Now,
						UserId = signedInUserId,
						
						Description = t.Description,
						DueDate = t.DueDate,
						ReminderDate = t.ReminderDate,
						Status = t.Status,
						TaskTags = t.TaskTags,
						Title = t.Title,
					}).ToList(),
					UserId = signedInUserId
				};

				await _appDbContext.AddTodoListAsync(newTodoList);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> CreateTaskAsync(TaskModel newTask)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(CreateTaskAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, newTask, nameof(newTask), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				await _appDbContext.CreateTaskAsync(newTask);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(ReadTaskAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TaskModel taskFromDb = new();

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				taskFromDb = await _appDbContext.ReadTaskAsync(taskId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, taskFromDb, nameof(taskFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return taskFromDb;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> UpdateTaskAsync(TaskModel taskToUpdate)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateTaskAsync), contextName);

			HelperCheck.IfArgumentModelNullThrowException(operationName, taskToUpdate, nameof(taskToUpdate), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
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

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> DeleteTaskAsync(int taskId, string signedInUserId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), contextName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref signedInUserId, nameof(signedInUserId), _logger);

			TryCatchBlockForDbDelType dbOperationsBlockAsync = new(async () =>
			{
				TaskModel? taskToDelete = await ReadTaskAsync(taskId, signedInUserId);
				HelperCheck.IfInstanceNullThrowException(operationName, taskToDelete, nameof(taskToDelete), _logger);
				await _appDbContext.DeleteTaskAsync(taskToDelete);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(dbOperationsBlockAsync);
			return true;
		}

		/// <inheritdoc/>
		public virtual async Task<bool> DoesTodoListWithSameNameExistAsync(string todoListName)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteTaskAsync), nameof(AppDbContext));

			if (string.IsNullOrEmpty(todoListName))
			{
				_logger.LogError(Messages.ParamNullOrEmptyLogger, operationName, nameof(todoListName));
				throw new ArgumentNullException(nameof(todoListName), Messages.ParamObjectNull(operationName, nameof(todoListName)));
			}

			return await _appDbContext.DoesTodoListExistByName(todoListName);
		}

		private async Task ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(TryCatchBlockForDbDelType dbOperationsBlockAsync)
		{
			methodName = dbOperationsBlockAsync.Method.Name;

			try
			{
				await dbOperationsBlockAsync.Invoke();
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

		private async Task<string> ProvideNameForDuplicateToDoList(string originalName, string signedInUserId)
		{
			StringBuilder nameBuilder = new(originalName);
			nameBuilder.Append(" - Copy");
			int iterator = startingValueForDuplication;

			while (await _appDbContext.TodoLists.AnyAsync(l => l.Name == nameBuilder.ToString() && l.UserId == signedInUserId))
			{
				if (Regex.IsMatch(nameBuilder.ToString(), @$"\d$"))
				{
					nameBuilder.Remove(nameBuilder.Length - iterator.ToString().Length, iterator.ToString().Length);
					nameBuilder.Append($" {iterator++}");
					continue;
				}
				nameBuilder.Append($" {iterator++}");
			}

			return nameBuilder.ToString();
		}
	}
}

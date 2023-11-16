using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;
using App.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.App;

///<inheritdoc />
public class TodoListRepository : GenericRepository<TodoListModel>, ITodoListRepository
{
	private readonly CustomAppDbContext _dbContext;
	private readonly ILogger<TodoListRepository> _logger;
	private string operationName = string.Empty;

	public TodoListRepository(CustomAppDbContext dbContext, ILogger<TodoListRepository> logger) : base(dbContext, logger)
	{
		_dbContext = dbContext;
		_logger = logger;
	}

	///<inheritdoc />
	public async Task<bool> CheckThatAnyWithSameNameExistAsync(string todoListName)
	{
		ExceptionsService.ThrowWhenArgumentIsInvalid(nameof(CheckThatAnyWithSameNameExistAsync), todoListName, nameof(todoListName), _logger);

		return await _dbContext.Set<TodoListModel>()
			.AnyAsync(todoList => todoList.Title == todoListName);
	}

	///<inheritdoc />
	public async Task DuplicateWithDetailsAsync(int todoListId)
	{
		ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(DuplicateWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		ITodoListModel? todoListWithDetails = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();

		if (todoListWithDetails is null)
			ExceptionsService.ThrowEntityNotFoundInDb(nameof(DuplicateWithDetailsAsync), typeof(ITodoListModel).Name, todoListId.ToString(), _logger);

	}
		{
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, "TodoLists");
			throw new InvalidOperationException(MessagesPacket.ExceptionNullObjectOnAction(operationName, nameof(todoListWithDetails)));
		}

		var tasksTemp = todoListWithDetails.Tasks.Select(t => new TaskModel()
		{
			Id = 0,
			CreationDate = DateTime.Now,
			LastModificationDate = DateTime.Now,
			UserId = t.UserId,
			Description = t.Description,
			DueDate = t.DueDate,
			ReminderDate = t.ReminderDate,
			Status = t.Status,
			TaskTags = t.TaskTags,
			Title = t.Title,
		}).Cast<ITaskModel>().ToList();

		TodoListModel newTodoList = new()
		{
			Id = 0,
			Title = todoListWithDetails.Title,
			Tasks = tasksTemp,
			UserId = todoListWithDetails.UserId
		};

		await AddAsync(newTodoList);
	}

	///<inheritdoc />
	public async Task<ICollection<TodoListModel>> GetAllWithDetailsAsync(string userId)
	{
		ExceptionsService.ThrowExceptionWhenArgumentIsNullOrEmpty(nameof(GetAllWithDetailsAsync), userId, nameof(userId), _logger);

		ICollection<TodoListModel> allTodoListsWithDetails = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.UserId == userId)
			.Include(todoList => todoList.Tasks)
			.ToListAsync();

		return allTodoListsWithDetails;
	}

	///<inheritdoc />
	public async Task<ICollection<TodoListModel>> GetAllWithDetailsByFilterAsync(Expression<Func<TodoListModel, bool>> filter)
	{
		ExceptionsService.ThrowWhenFilterExpressionIsNull(filter, nameof(GetAllWithDetailsByFilterAsync), _logger);

		ICollection<TodoListModel> entities = await _dbContext
			.Set<TodoListModel>()
			.Where(filter)
			.Include(todoList => todoList.Tasks)
			.ToListAsync();

		return entities;
	}

	///<inheritdoc />
	public async Task<TodoListModel?> GetWithDetailsAsync(int todoListId)
	{
		ExceptionsService.ThrowWhenArgumentIsInvalid(nameof(GetWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListFromDb = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();

		return todoListFromDb;
	}
}

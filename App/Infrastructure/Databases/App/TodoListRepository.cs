using App.Features.Exceptions.Throw;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.App;

///<inheritdoc />
public class TodoListRepository : GenericRepository<TodoListModel>, ITodoListRepository
{
	private readonly CustomAppDbContext _dbContext;
	private readonly ILogger<TodoListRepository> _logger;
	private readonly ITaskEntityFactory _taskEntityFactory;
	private readonly ITodoListFactory _todoListFactory;

	public TodoListRepository(CustomAppDbContext dbContext, ILogger<TodoListRepository> logger, ITaskEntityFactory taskEntityFactory, ITodoListFactory todoListFactory)
		: base(dbContext, logger)
	{
		_dbContext = dbContext;
		_logger = logger;
		_taskEntityFactory = taskEntityFactory;
		_todoListFactory = todoListFactory;
	}

	///<inheritdoc />
	public async Task DuplicateWithDetailsAsync(int todoListId)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DuplicateWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListWithDetails = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(DuplicateWithDetailsAsync), todoListWithDetails, _logger, todoListId);

		var duplicatedTasks = todoListWithDetails!.Tasks.Select(originTask => CreateNewTaskObject(originTask)).ToList();
		var duplicatedTodoList = CreateNewTodoListObject(todoListWithDetails, duplicatedTasks);

		await AddAsync(duplicatedTodoList);
	}


	#region LOCAL FUNCTIONS FOR DUPLICATE OPERATION

	private TaskModel CreateNewTaskObject(TaskModel originTask)
	{
		return _taskEntityFactory.CreateTaskModel(originTask);
	}

	private TodoListModel CreateNewTodoListObject(TodoListModel originTodoList, ICollection<TaskModel> newTasks)
	{
		return _todoListFactory.CreateModel(originTodoList, newTasks);
	}

	#endregion


	///<inheritdoc />
	public IQueryable<TodoListModel> GetAllWithDetails(string userId)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetAllWithDetails), userId, nameof(userId), _logger);

		IQueryable<TodoListModel> allTodoListsWithDetails = _dbContext
			.Set<TodoListModel>()
			.AsQueryable()
			.Where(todoList => todoList.UserId == userId)
			.Include(todoList => todoList.Tasks);

		return allTodoListsWithDetails;
	}

	///<inheritdoc />
	public IQueryable<TodoListModel> GetAllWithDetailsByFilter(Expression<Func<TodoListModel, bool>> filter)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetAllWithDetailsByFilter), _logger);

		IQueryable<TodoListModel> entities = _dbContext
			.Set<TodoListModel>()
			.AsQueryable()
			.Where(filter)
			.Include(todoList => todoList.Tasks);

		return entities;
	}

	///<inheritdoc />
	public async Task<TodoListModel?> GetWithDetailsAsync(int todoListId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListFromDb = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();

		return todoListFromDb;
	}
}

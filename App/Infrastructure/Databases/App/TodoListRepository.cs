using App.Features.Exceptions.Throw;
using App.Features.Pagination;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
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
	private readonly DbSet<TodoListModel> _dbSet;

	public TodoListRepository(CustomAppDbContext dbContext, ILogger<TodoListRepository> logger, ITaskEntityFactory taskEntityFactory, ITodoListFactory todoListFactory)
		: base(dbContext, logger)
	{
		_dbContext = dbContext;
		_logger = logger;
		_taskEntityFactory = taskEntityFactory;
		_todoListFactory = todoListFactory;

		_dbSet = _dbContext.Set<TodoListModel>();
	}

	///<inheritdoc />
	public async Task DuplicateSingleWithDetailsAsync(int todoListId)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(DuplicateSingleWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListWithDetails = await _dbSet
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();
		ExceptionsService.WhenEntityIsNullThrow(nameof(DuplicateSingleWithDetailsAsync), todoListWithDetails, _logger, todoListId);

		var duplicatedTasks = todoListWithDetails!.Tasks.Select(originTask => CreateNewTaskObject(originTask)).ToList();
		var duplicatedTodoList = CreateNewTodoListObject(todoListWithDetails, duplicatedTasks);

		await AddAsync(duplicatedTodoList);
	}


	#region LOCAL FUNCTIONS FOR DUPLICATE OPERATION

	private TaskModel CreateNewTaskObject(TaskModel originTask)
	{
		var newTask = _taskEntityFactory.CreateModel();
		newTask.UserId = originTask.UserId;
		newTask.Title = originTask.Title;
		newTask.Description = originTask.Description;
		newTask.DueDate = originTask.DueDate;
		newTask.ReminderDate = originTask.ReminderDate;
		newTask.Status = originTask.Status;

		newTask.TaskTags = originTask.TaskTags.Select(originTaskTag => CreateNewTaskTagObject(originTaskTag)).ToList();

		return newTask;
	}

	private TaskTagModel CreateNewTaskTagObject(TaskTagModel originTaskTag)
	{
		var newTaskTag = _taskEntityFactory.CreateTaskTagModel();
		newTaskTag.TagId = originTaskTag.TagId;
		newTaskTag.TaskId = originTaskTag.TaskId;

		return newTaskTag;
	}

	private TodoListModel CreateNewTodoListObject(TodoListModel originTodoList, ICollection<TaskModel> newTasks)
	{
		TodoListModel newTodoList = _todoListFactory.CreateModel();
		newTodoList.Title = originTodoList.Title + "###";
		newTodoList.Tasks = newTasks;
		newTodoList.UserId = originTodoList.UserId;

		return newTodoList;
	}

	#endregion


	///<inheritdoc />
	public async Task<ICollection<TodoListModel>> GetMultipleWithDetailsAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetMultipleWithDetailsAsync), userId, nameof(userId), _logger);

		ICollection<TodoListModel> allTodoListsWithDetails = await _dbSet
			.Where(todoList => todoList.UserId == userId)
			.Include(todoList => todoList.Tasks)
			.ToListAsync();

		return allTodoListsWithDetails;
	}

	///<inheritdoc />
	public async Task<ICollection<TodoListModel>> GetMultipleWithDetailsByFilterAsync(Expression<Func<TodoListModel, bool>> filter)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetMultipleWithDetailsByFilterAsync), _logger);

		ICollection<TodoListModel> entities = await _dbSet
			.Where(filter)
			.Include(todoList => todoList.Tasks)
			.ToListAsync();

		return entities;
	}

	///<inheritdoc />
	public async Task<TodoListModel?> GetSingleWithDetailsAsync(int todoListId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetSingleWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListFromDb = await _dbSet
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();

		return todoListFromDb;
	}

	public async Task<TodoListModel?> GetSingleWithDetailsAsync(int todoListId, Expression<Func<TaskModel, object>> orderDetailsBySelector, int pageNumber, int itemsPerPageCount)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(GetSingleWithDetailsAsync), todoListId, nameof(todoListId), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetSingleWithDetailsAsync), pageNumber, nameof(pageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetSingleWithDetailsAsync), itemsPerPageCount, nameof(itemsPerPageCount), _logger);

		int skipAmount = PaginationHelper.CountItemsToSkip(pageNumber, itemsPerPageCount, _logger);

		var query = _dbSet
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks
				.AsQueryable()
				.OrderBy(orderDetailsBySelector)
				.Skip(skipAmount)
				.Take(itemsPerPageCount));

		var todoList = await query.SingleOrDefaultAsync();
		return todoList;
	}

	public async Task<ICollection<TodoListModel>> GetMultipleWithDetailsAsync(string userId, int pageNumber, int itemsPerPageCount, Expression<Func<TodoListModel, object>> orderBySelector, Expression<Func<TaskModel, object>> orderDetailsBySelector)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetMultipleWithDetailsAsync), userId, nameof(userId), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetMultipleWithDetailsAsync), pageNumber, nameof(pageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetMultipleWithDetailsAsync), itemsPerPageCount, nameof(itemsPerPageCount), _logger);

		int skipAmount = PaginationHelper.CountItemsToSkip(pageNumber, itemsPerPageCount, _logger);

		IQueryable<TodoListModel> query = _dbSet
					.Where(todoList => todoList.UserId == userId)
					.Include(todoList => todoList.Tasks.AsQueryable()
						.OrderBy(orderDetailsBySelector))
					.OrderBy(orderBySelector)
					.Skip(skipAmount)
					.Take(itemsPerPageCount);

		return await query.ToListAsync();
	}

	public IQueryable<TodoListModel> GetMultipleByFilter(Expression<Func<TodoListModel, bool>> filter, Expression<Func<TodoListModel, object>> orderBySelector, int pageNumber, int itemsPerPageCount)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetMultipleWithDetailsByFilterAsync), _logger);

		int skipAmount = PaginationHelper.CountItemsToSkip(pageNumber, itemsPerPageCount, _logger);

		var query = _dbSet
			.Where(filter)
			.OrderBy(orderBySelector)
			.Skip(skipAmount)
			.Take(itemsPerPageCount);

		return query;
	}
}

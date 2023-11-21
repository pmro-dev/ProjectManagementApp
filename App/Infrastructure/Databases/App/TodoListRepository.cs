using App.Features.Tasks.Common;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
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
	public async Task<bool> CheckThatAnyWithSameNameExistAsync(string todoListName)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(CheckThatAnyWithSameNameExistAsync), todoListName, nameof(todoListName), _logger);

		return await _dbContext.Set<TodoListModel>()
			.AnyAsync(todoList => todoList.Title == todoListName);
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

		if (todoListWithDetails is null)
			ExceptionsService.ThrowCriticalEntityNotFoundInDb(nameof(DuplicateWithDetailsAsync), typeof(TodoListModel).Name, todoListId.ToString(), _logger);

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
		newTodoList.Title = originTodoList.Title;
		newTodoList.Tasks = newTasks;
		newTodoList.UserId = originTodoList.UserId;

		return newTodoList;
	}

	#endregion


	///<inheritdoc />
	public async Task<ICollection<TodoListModel>> GetAllWithDetailsAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(GetAllWithDetailsAsync), userId, nameof(userId), _logger);

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
		ExceptionsService.ThrowErrorFilterExpressionIsNull(filter, nameof(GetAllWithDetailsByFilterAsync), _logger);

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
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetWithDetailsAsync), todoListId, nameof(todoListId), _logger);

		TodoListModel? todoListFromDb = await _dbContext
			.Set<TodoListModel>()
			.Where(todoList => todoList.Id == todoListId)
			.Include(todoList => todoList.Tasks)
			.SingleOrDefaultAsync();

		return todoListFromDb;
	}
}

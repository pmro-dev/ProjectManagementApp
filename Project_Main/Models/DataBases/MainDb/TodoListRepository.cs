﻿using Microsoft.EntityFrameworkCore;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
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
		public async Task<bool> DoesAnyExistWithSameNameAsync(string name)
        {
            return await _dbContext.Set<TodoListModel>()
                .AnyAsync(todoList => todoList.Title == name);
        }

		///<inheritdoc />
		public async Task DuplicateWithDetailsAsync(int id)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(DuplicateWithDetailsAsync), nameof(TodoListRepository));
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            TodoListModel? todoListWithDetails = await _dbContext
                .Set<TodoListModel>()
                .Where(todoList => todoList.Id == id)
                .Include(todoList => todoList.Tasks)
                .SingleOrDefaultAsync();

            if (todoListWithDetails is null)
            {
                _logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, id, "TodoLists");
                throw new InvalidOperationException(Messages.ExceptionNullObjectOnAction(operationName, nameof(todoListWithDetails)));
            }

            TodoListModel newTodoList = new()
            {
                Id = 0,
                Title = todoListWithDetails.Title,
                Tasks = todoListWithDetails.Tasks.Select(t => new TaskModel()
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
                }).ToList(),
                UserId = todoListWithDetails.UserId
            };

            await AddAsync(newTodoList);
        }

		///<inheritdoc />
		public async Task<IEnumerable<TodoListModel>> GetAllWithDetailsAsync(string userId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetAllWithDetailsAsync), nameof(TodoListRepository));
            HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userId, nameof(userId), _logger);

			IEnumerable<TodoListModel> allTodoListsWithDetails = await _dbContext
                .Set<TodoListModel>()
                .Where(todoList => todoList.UserId == userId)
                .Include(todoList => todoList.Tasks).ToListAsync();

            return allTodoListsWithDetails;
        }

		///<inheritdoc />
		public async Task<TodoListModel?> GetWithDetailsAsync(int id)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), nameof(TodoListRepository));
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            TodoListModel? todoListFromDb = await _dbContext
                .Set<TodoListModel>()
                .Where(todoList => todoList.Id == id)
                .Include(todoList => todoList.Tasks)
                .SingleOrDefaultAsync();

            return todoListFromDb;
        }
    }
}

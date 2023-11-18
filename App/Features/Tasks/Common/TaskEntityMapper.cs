using App.Features.Tags.Common;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.Tasks.Create;
using App.Features.Tasks.Delete;
using App.Features.Tasks.Edit;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Tasks.Common;

public class TaskEntityMapper : ITaskEntityMapper
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ITaskEntityFactory _taskEntityFactory;
	private readonly ILogger<TaskEntityMapper> _logger;

	public TaskEntityMapper(IServiceProvider serviceProvider, ILogger<TaskEntityMapper> logger, ITaskEntityFactory taskEntityFactory)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
		_taskEntityFactory = taskEntityFactory;
	}

	public TaskDeleteInputDto TransferToDto(TaskDeleteInputVM deleteInputVM)
	{
		var deleteInputDto = _taskEntityFactory.CreateDeleteInputDto();

		deleteInputDto.Id = deleteInputVM.Id;
		deleteInputDto.TodoListId = deleteInputVM.TodoListId;

		return deleteInputDto;
	}

	public TaskDto TransferToDto(TaskCreateInputVM taskInputVM)
	{
		var taskDto = _taskEntityFactory.CreateDto();

		taskDto.Title = taskInputVM.Title;
		taskDto.Description = taskInputVM.Description;
		taskDto.DueDate = taskInputVM.DueDate;
		taskDto.ReminderDate = taskInputVM.ReminderDate;
		taskDto.UserId = taskInputVM.UserId;
		taskDto.TodoListId = taskInputVM.TodoListId;

		return taskDto;
	}

	public TaskEditInputDto TransferToDto(TaskEditInputVM taskEditInputVM)
	{
		var editInputDto = _taskEntityFactory.CreateEditInputDto();

		editInputDto.Id = taskEditInputVM.Id;
		editInputDto.Title = taskEditInputVM.Title;
		editInputDto.Description = taskEditInputVM.Description;
		editInputDto.DueDate = taskEditInputVM.DueDate;
		editInputDto.ReminderDate = taskEditInputVM.ReminderDate;
		editInputDto.Status = taskEditInputVM.Status;
		editInputDto.TodoListId = taskEditInputVM.TodoListId;
		editInputDto.UserId = taskEditInputVM.UserId;

		return editInputDto;
	}


	#region Map TaskModel To TaskDto

	public TaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTaskToDto(taskModel, mappedObjects ?? new Dictionary<object, object>());
	}

	private TaskDto MapTaskToDto(TaskModel taskModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskModel, out var mappedObject))
			return (TaskDto)mappedObject;

		TaskDto taskDto = _taskEntityFactory.CreateDto();
		taskDto.Id = taskModel.Id;
		taskDto.CreationDate = taskModel.CreationDate;
		taskDto.Description = taskModel.Description;
		taskDto.DueDate = taskModel.DueDate;
		taskDto.LastModificationDate = taskModel.LastModificationDate;
		taskDto.ReminderDate = taskModel.ReminderDate;
		taskDto.Status = taskModel.Status;
		taskDto.Title = taskModel.Title;
		taskDto.TodoListId = taskModel.TodoListId;
		taskDto.UserId = taskModel.UserId;

		mappedObjects[taskModel] = taskDto;

		taskDto.TaskTags = MapMultipleTaskTagsToDtos(taskModel.TaskTags, mappedObjects);

		if (taskModel.TodoList is not null)
		{
			ITodoListMapper todoListMapper = GetTodoListDtoService();
			taskDto.TodoList = todoListMapper.TransferToDto(taskModel.TodoList, mappedObjects);
		}

		return taskDto;
	}

	private ITodoListMapper GetTodoListDtoService()
	{
		return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITodoListMapper>();
	}

	private ICollection<TaskTagDto> MapMultipleTaskTagsToDtos(ICollection<TaskTagModel> taskTagModels, IDictionary<object, object> mappedObjects)
	{
		return taskTagModels.Select(taskTag => MapTaskTagToDto(taskTag, mappedObjects)).ToList();
	}

	private TagDto MapTagToDto(TagModel tagModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(tagModel, out var mappedObject))
			return (TagDto)mappedObject;

		TagDto tagDto = _taskEntityFactory.CreateTagDto();
		tagDto.Id = tagModel.Id;
		tagDto.Title = tagModel.Title;

		mappedObjects[tagModel] = tagDto;

		tagDto.TaskTags = MapMultipleTaskTagsToDtos(tagModel.TaskTags, mappedObjects);

		return tagDto;
	}

	private TaskTagDto MapTaskTagToDto(TaskTagModel taskTagModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskTagModel, out var mappedObject))
			return (TaskTagDto)mappedObject;

		var taskTagDto = _taskEntityFactory.CreateTaskTagDto();
		taskTagDto.TaskId = taskTagModel.TaskId;
		taskTagDto.TagId = taskTagModel.TagId;

		mappedObjects[taskTagModel] = taskTagDto;

		taskTagDto.Task = MapTaskToDto(taskTagModel.Task, mappedObjects);
		taskTagDto.Tag = MapTagToDto(taskTagModel.Tag, mappedObjects);

		return taskTagDto;
	}

	#endregion


	#region Map TaskDto to TaskModel

	public TaskModel TransferToModel(TaskDto taskDto, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTaskToModel(taskDto, mappedObjects ?? new Dictionary<object, object>());
	}

	private TaskModel MapTaskToModel(TaskDto taskDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskDto, out var mappedObject))
			return (TaskModel)mappedObject;

		TaskModel taskModel = _taskEntityFactory.CreateModel();
		taskModel.Id = taskDto.Id;
		taskModel.CreationDate = taskDto.CreationDate;
		taskModel.Description = taskDto.Description;
		taskModel.DueDate = taskDto.DueDate;
		taskModel.LastModificationDate = taskDto.LastModificationDate;
		taskModel.ReminderDate = taskDto.ReminderDate;
		taskModel.Status = taskDto.Status;
		taskModel.Title = taskDto.Title;
		taskModel.TodoListId = taskDto.TodoListId;
		taskModel.UserId = taskDto.UserId;

		mappedObjects[taskDto] = taskModel;

		taskModel.TaskTags = MapMultipleTaskTagsToModels(taskDto.TaskTags, mappedObjects);

		if (taskDto.TodoList is not null)
		{
			ITodoListMapper todoListMapper = GetTodoListDtoService();
			taskModel.TodoList = todoListMapper.TransferToModel(taskDto.TodoList, mappedObjects);
		}

		return taskModel;
	}

	private ICollection<TaskTagModel> MapMultipleTaskTagsToModels(ICollection<TaskTagDto> taskTagDtos, IDictionary<object, object> mappedObjects)
	{
		return taskTagDtos.Select(taskTag => MapTaskTagToModel(taskTag, mappedObjects)).ToList();
	}

	private TaskTagModel MapTaskTagToModel(TaskTagDto taskTagDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskTagDto, out var mappedObject))
			return (TaskTagModel)mappedObject;

		TaskTagModel taskTagModel = _taskEntityFactory.CreateTaskTagModel();
		taskTagModel.TagId = taskTagDto.TagId;
		taskTagModel.TaskId = taskTagDto.TaskId;

		mappedObjects[taskTagDto] = taskTagModel;

		taskTagModel.Task = MapTaskToModel(taskTagDto.Task, mappedObjects);
		taskTagModel.Tag = MapTagToModel(taskTagDto.Tag, mappedObjects);

		return taskTagModel;
	}

	private TagModel MapTagToModel(TagDto tagDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(tagDto, out var mappedObject))
			return (TagModel)mappedObject;

		TagModel tagModel = _taskEntityFactory.CreateTagModel();
		tagModel.Id = tagDto.Id;
		tagModel.Title = tagDto.Title;

		mappedObjects[tagDto] = tagModel;

		tagModel.TaskTags = MapMultipleTaskTagsToModels(tagDto.TaskTags, mappedObjects);

		return tagModel;
	}

    #endregion


    public void UpdateModel(TaskModel taskDbModel, TaskEditInputDto taskEditInputDto)
    {
        taskDbModel.Id = taskEditInputDto.Id;
        taskDbModel.Title = taskEditInputDto.Title;
        taskDbModel.Description = taskEditInputDto.Description;
        taskDbModel.DueDate = taskEditInputDto.DueDate;
        taskDbModel.ReminderDate = taskEditInputDto.ReminderDate;
        taskDbModel.Status = taskEditInputDto.Status;
        taskDbModel.TodoListId = taskEditInputDto.TodoListId;
        taskDbModel.UserId = taskEditInputDto.UserId;
    }
}

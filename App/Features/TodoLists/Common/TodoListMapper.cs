using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Edit.Models;

namespace App.Features.TodoLists.Common;

public class TodoListMapper : ITodoListMapper
{
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITodoListFactory _todoListFactory;

	public TodoListMapper(ITaskEntityMapper taskEntityMapper, ITodoListFactory todoListFactory)
	{
		_taskEntityMapper = taskEntityMapper;
		_todoListFactory = todoListFactory;
	}


	#region TRANSFER DTO TO MODEL

	public TodoListModel TransferToModel(TodoListDto todoListDto, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTodoListToModel(todoListDto, mappedObjects ?? new Dictionary<object, object>());
	}

	private TodoListModel MapTodoListToModel(TodoListDto todoListDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(todoListDto, out var mappedObject))
			return (TodoListModel)mappedObject;

		var todoListModel = _todoListFactory.CreateModel();
		todoListModel.Id = todoListDto.Id;
		todoListModel.Title = todoListDto.Title;
		todoListModel.UserId = todoListDto.UserId;

		mappedObjects[todoListDto] = todoListModel;

		todoListModel.Tasks = MapMultipleTasksToModels(todoListDto.Tasks, mappedObjects);

		return todoListModel;
	}

	private ICollection<TaskModel> MapMultipleTasksToModels(ICollection<TaskDto> taskDtos, IDictionary<object, object> mappedObjects)
	{
		return taskDtos.Select(task => _taskEntityMapper.TransferToModel(task, mappedObjects)).ToList();
	}

	#endregion


	public TodoListDto TransferToDto(TodoListCreateInputVM createInputVM)
	{
		var todoListDto = _todoListFactory.CreateDto();
		todoListDto.UserId = createInputVM.UserId;
		todoListDto.Title = createInputVM.Title;

		return todoListDto;
	}

	public TodoListEditInputDto TransferToDto(TodoListEditInputVM editInputVM)
	{
		var editInputDto = _todoListFactory.CreateEditInputDto();
		editInputDto.Title = editInputVM.Title;

		return editInputDto;
	}


	#region TRANSFER MODEL TO DTO

	public TodoListDto TransferToDto(TodoListModel todoListModel, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTodoListToDto(todoListModel, mappedObjects ?? new Dictionary<object, object>());
	}

	public ICollection<TodoListDto> TransferToDto(ICollection<TodoListModel> todoLists)
	{
		var mappedObjects = new Dictionary<object, object>();

		var todoListDtos = todoLists.Select(list => MapTodoListToDto(list, mappedObjects)).ToList();

		return todoListDtos;
	}

	private TodoListDto MapTodoListToDto(TodoListModel todoListModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(todoListModel, out var mappedObject))
			return (TodoListDto)mappedObject;

		var todoListDto = _todoListFactory.CreateDto();
		todoListDto.Id = todoListModel.Id;
		todoListDto.Title = todoListModel.Title;
		todoListDto.UserId = todoListModel.UserId;

		mappedObjects[todoListModel] = todoListDto;

		todoListDto.Tasks = MapMultipleTasksToDtos(todoListModel.Tasks, mappedObjects);

		return todoListDto;
	}

	private ICollection<TaskDto> MapMultipleTasksToDtos(ICollection<TaskModel> taskModels, IDictionary<object, object> mappedObjects)
	{
		return taskModels.Select(task => _taskEntityMapper.TransferToDto(task, mappedObjects)).ToList();
	}

	#endregion


	public void UpdateModel(TodoListModel todoListDbModel, TodoListEditInputDto taskEditInputDto)
	{
		todoListDbModel.Title = taskEditInputDto.Title;
	}
}

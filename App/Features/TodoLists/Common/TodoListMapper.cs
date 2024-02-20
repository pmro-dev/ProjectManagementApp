using App.Features.Exceptions.Throw;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Tags;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Edit.Models;
using AutoMapper;

namespace App.Features.TodoLists.Common;

public class TodoListMapper : ITodoListMapper
{
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITodoListFactory _todoListFactory;
	private readonly IMapper _mapper;
	private readonly ILogger<TodoListMapper> _logger;

	public TodoListMapper(ITaskEntityMapper taskEntityMapper, ITodoListFactory todoListFactory, IMapper mapper, ILogger<TodoListMapper> logger)
	{
		_taskEntityMapper = taskEntityMapper;
		_todoListFactory = todoListFactory;
		_mapper = mapper;
		_logger = logger;
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
		todoListModel.OwnerId = todoListDto.OwnerId;

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
		todoListDto.OwnerId = createInputVM.UserId;
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
		todoListDto.RowVersion = todoListModel.RowVersion;
		todoListDto.Title = todoListModel.Title;


		ExceptionsService.WhenPropertyIsNullOrEmptyThrow(nameof(MapTodoListToDto), todoListModel.OwnerId, nameof(todoListModel.OwnerId), _logger);
		todoListDto.OwnerId = todoListModel.OwnerId!;


		ExceptionsService.WhenPropertyIsNullOrEmptyThrow(nameof(MapTodoListToDto), todoListModel.CreatorId, nameof(todoListModel.CreatorId), _logger);
		todoListDto.CreatorId = todoListModel.CreatorId!;


		todoListDto.ProjectId = todoListModel.ProjectId;
		todoListDto.Project = MapProjectToDto(todoListModel.Project);

		todoListDto.TeamId = todoListModel.TeamId;
		todoListDto.Team = MapTeamToDto(todoListModel.Team);

		todoListDto.Tags = MapMultipleTagsToDtos(todoListModel.Tags);
		todoListDto.TodoListTags = MapMultipleTodoListTagsToDtos(todoListModel.TodoListTags);

		mappedObjects[todoListModel] = todoListDto;

		todoListDto.Tasks = MapMultipleTasksToDtos(todoListModel.Tasks, mappedObjects);

		return todoListDto;
	}

	private ICollection<TaskDto> MapMultipleTasksToDtos(ICollection<TaskModel> taskModels, IDictionary<object, object> mappedObjects)
	{
		return taskModels.Select(task => _taskEntityMapper.TransferToDto(task, mappedObjects)).ToList();
	}

	private ProjectDto? MapProjectToDto(ProjectModel? projectModel)
	{
		if (projectModel is null)
			return null;

		ProjectDto? projectDto = _mapper.Map<ProjectDto>(projectModel);

		return projectDto;
	}

	private TeamDto? MapTeamToDto(TeamModel? teamModel)
	{
		if (teamModel is null)
			return null;

		TeamDto? teamDto = _mapper.Map<TeamDto>(teamModel);
		return teamDto;
	}

	private ICollection<TagDto> MapMultipleTagsToDtos(ICollection<TagModel> tags)
	{
		return tags.Select(tag => _mapper.Map<TagDto>(tag)).ToList();
	}

	private ICollection<TodoListTagDto> MapMultipleTodoListTagsToDtos(ICollection<TodoListTagModel> todoListTags)
	{
		return todoListTags.Select(tltg => _mapper.Map<TodoListTagDto>(tltg)).ToList();
	}

	#endregion


	public void UpdateModel(TodoListModel todoListDbModel, TodoListEditInputDto taskEditInputDto)
	{
		todoListDbModel.Title = taskEditInputDto.Title;
	}
}

using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Factories.DTOs;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.Inputs.ViewModels;

namespace Project_Main.Services.DTO
{
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

		public TodoListModel TransferToModel(ITodoListDto todoListDto, IDictionary<object, object>? mappedObjects = null)
		{
			return MapTodoListToModel(todoListDto, mappedObjects ?? new Dictionary<object, object>());
		}

		private TodoListModel MapTodoListToModel(ITodoListDto todoListDto, IDictionary<object, object> mappedObjects)
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

		private ICollection<ITaskModel> MapMultipleTasksToModels(ICollection<ITaskDto> taskDtos, IDictionary<object, object> mappedObjects)
		{
			return taskDtos.Select(task => _taskEntityMapper.TransferToModel(task, mappedObjects)).Cast<ITaskModel>().ToList();
		}

		#endregion


		public ITodoListDto TransferToDto(ITodoListCreateInputVM createInputVM)
		{
			var todoListDto = _todoListFactory.CreateDto();
			todoListDto.UserId = createInputVM.UserId;
			todoListDto.Title = createInputVM.Title;

			return todoListDto;
		}

		public ITodoListEditInputDto TransferToDto(ITodoListEditInputVM editInputVM)
		{
			var editInputDto = _todoListFactory.CreateEditInputDto();
			editInputDto.Title = editInputVM.Title;

			return editInputDto;
		}


		#region TRANSFER MODEL TO DTO

		public ITodoListDto TransferToDto(ITodoListModel todoListModel, IDictionary<object, object>? mappedObjects = null)
		{
			return MapTodoListToDto(todoListModel, mappedObjects ?? new Dictionary<object, object>());
		}

		public ICollection<ITodoListDto> TransferToDto(ICollection<TodoListModel> todoLists)
		{
			var mappedObjects = new Dictionary<object, object>();

			var todoListDtos = todoLists.Select(list => MapTodoListToDto(list, mappedObjects)).ToList();

			return todoListDtos;
		}

		private ITodoListDto MapTodoListToDto(ITodoListModel todoListModel, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(todoListModel, out var mappedObject))
				return (ITodoListDto)mappedObject;

			var todoListDto = _todoListFactory.CreateDto();
			todoListDto.Id = todoListModel.Id;
			todoListDto.Title = todoListModel.Title;
			todoListDto.UserId = todoListModel.UserId;

			mappedObjects[todoListModel] = todoListDto;

			todoListDto.Tasks = MapMultipleTasksToDtos(todoListModel.Tasks, mappedObjects);

			return todoListDto;
		}

		private ICollection<ITaskDto> MapMultipleTasksToDtos(ICollection<ITaskModel> taskModels, IDictionary<object, object> mappedObjects)
		{
			return taskModels.Select(task => _taskEntityMapper.TransferToDto((TaskModel)task, mappedObjects)).ToList();
		}

		#endregion


		public void UpdateModel(TodoListModel todoListDbModel, ITodoListEditInputDto taskEditInputDto)
		{
			todoListDbModel.Title = taskEditInputDto.Title;
		}
	}
}

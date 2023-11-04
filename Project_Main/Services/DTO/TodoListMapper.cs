using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO
{
    public class TodoListMapper : ITodoListMapper
    {
        private readonly ITaskEntityMapper _taskEntityMapper;

        public TodoListMapper(ITaskEntityMapper taskEntityMapper)
        {
            _taskEntityMapper = taskEntityMapper;
        }


        #region TRANSFER DTO TO MODEL

        public ITodoListModel TransferToModel(ITodoListDto todoListDto, Dictionary<object, object>? mappedObjects = null)
        {
            return MapTodoListToModel(todoListDto, mappedObjects ?? new Dictionary<object, object>());
        }

        private ITodoListModel MapTodoListToModel(ITodoListDto todoListDto, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(todoListDto, out var mappedObject))
                return (ITodoListModel)mappedObject;

            var todoListModel = new TodoListModel()
            {
                Id = todoListDto.Id,
                Title = todoListDto.Title,
                UserId = todoListDto.UserId,
            };

            mappedObjects[todoListDto] = todoListModel;

            todoListModel.Tasks = MapMultipleTasksToModels(todoListDto.Tasks, mappedObjects);

            return todoListModel;
        }

        private ICollection<ITaskModel> MapMultipleTasksToModels(ICollection<ITaskDto> taskDtos, Dictionary<object, object> mappedObjects)
        {
            return taskDtos.Select(task => _taskEntityMapper.TransferToModel(task, mappedObjects)).ToList();
        }

		#endregion


		public ITodoListDto TransferToDto(ITodoListCreateInputVM createInputVM)
		{
            return new TodoListDto()
            {
                UserId = createInputVM.UserId,
                Title = createInputVM.Title
            };
		}

        public ITodoListEditInputDto TransferToDto(ITodoListEditInputVM editInputVM)
        {
            return new TodoListEditInputDto()
            {
                Title = editInputVM.Title
            };
        }


        #region TRANSFER MODEL TO DTO

        public ITodoListDto TransferToDto(ITodoListModel todoListModel, Dictionary<object, object>? mappedObjects = null)
        {
            return MapTodoListToDto(todoListModel, mappedObjects ?? new Dictionary<object, object>());
        }

        public ICollection<ITodoListDto> TransferToDto(ICollection<ITodoListModel> todoLists) 
        {
            var mappedObjects = new Dictionary<object, object>();

            var todoListDtos = todoLists.Select(list => MapTodoListToDto(list, mappedObjects)).ToList();

            return todoListDtos;
        }

        private ITodoListDto MapTodoListToDto(ITodoListModel todoListModel, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(todoListModel, out var mappedObject))
                return (ITodoListDto)mappedObject;

            var todoListDto = new TodoListDto()
            {
                Id = todoListModel.Id,
                Title = todoListModel.Title,
                UserId = todoListModel.UserId,
            };

            mappedObjects[todoListModel] = todoListDto;

            todoListDto.Tasks = MapMultipleTasksToDtos(todoListModel.Tasks, mappedObjects);

            return todoListDto;
        }

        private ICollection<ITaskDto> MapMultipleTasksToDtos(ICollection<ITaskModel> taskModels, Dictionary<object, object> mappedObjects)
        {
            return taskModels.Select(task => _taskEntityMapper.TransferToDto(task, mappedObjects)).ToList();
        }

        #endregion


        public void UpdateModel(ITodoListModel todoListDbModel, ITodoListEditInputDto taskEditInputDto)
        {
            todoListDbModel.Title = taskEditInputDto.Title;
        }
	}
}

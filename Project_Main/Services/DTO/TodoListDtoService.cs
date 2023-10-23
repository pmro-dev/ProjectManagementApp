using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO
{
    public static class TodoListDtoService
    {
        public static TodoListModelDto TransferToDto(TodoListModel todoListModel)
        {
            return new TodoListModelDto
            {
                Id = todoListModel.Id,
                Title = todoListModel.Title,
                UserId = todoListModel.UserId,
                Tasks = todoListModel.Tasks.Select(t => t).ToList()
            };
        }

        public static BoardsAllOutputVM TransferToBoardsAllOutputVM(TodoListModelDto todoListDto)
        {
            return new BoardsAllOutputVM
            {
                Id = todoListDto.Id,
                Title = todoListDto.Title,
                UserId = todoListDto.UserId,
                Tasks = todoListDto.Tasks.Select(t => t).ToList()
		public static BoardsAllOutputVM TransferToBoardsAllOutputVM(IEnumerable<TodoListModelDto> todoListDtos)
		{
			return new BoardsAllOutputVM
			{
				TodoListDtos = todoListDtos.Select(todoList =>
				new TodoListModelDto()
				{
					Id = todoList.Id,
					Title = todoList.Title,
					UserId = todoList.UserId,
					Tasks = todoList.Tasks
				})
			};
		}
            };
        }

        public static BoardsSingleDetailsOutputVM TransferToSingleDetailsOutputVM(TodoListModelDto todoListDto, DateTime? filterDueDate = null)
        {
            return new BoardsSingleDetailsOutputVM
            {
                Id = todoListDto.Id,
                Name = todoListDto.Title,
                TasksForToday = TasksFilterService.FilterForTasksForToday(todoListDto.Tasks),
                TasksCompleted = TasksFilterService.FilterForTasksCompleted(todoListDto.Tasks),
                TasksNotCompleted = TasksFilterService.FilterForTasksNotCompleted(todoListDto.Tasks, filterDueDate),
                TasksExpired = TasksFilterService.FilterForTasksExpired(todoListDto.Tasks)
            };
        }
    }
}

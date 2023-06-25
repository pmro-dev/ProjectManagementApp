using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Services
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
                Tasks = todoListModel.Tasks
            };
        }
    }
}

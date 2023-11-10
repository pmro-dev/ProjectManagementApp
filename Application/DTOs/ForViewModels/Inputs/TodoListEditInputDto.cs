using Application.DTOs.ViewModels.Inputs.Abstract;

namespace Application.DTOs.ForViewModels.Inputs;

public class TodoListEditInputDto : ITodoListEditInputDto
{
    public string Title { get; set; } = string.Empty;
}
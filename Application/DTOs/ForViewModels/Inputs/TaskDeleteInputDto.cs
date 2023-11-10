using Application.DTOs.ViewModels.Inputs.Abstract;

namespace Application.DTOs.ForViewModels.Inputs;

public class TaskDeleteInputDto : ITaskDeleteInputDto
{
    public int Id { get; set; }
    public int TodoListId { get; set; }
}

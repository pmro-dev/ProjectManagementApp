namespace Application.DTOs.ViewModels.Inputs.Abstract
{
    public interface ITaskDeleteInputDto
    {
        int Id { get; set; }
        int TodoListId { get; set; }
    }
}
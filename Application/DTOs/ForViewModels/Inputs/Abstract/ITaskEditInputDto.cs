using Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ViewModels.Inputs.Abstract
{
    public interface ITaskEditInputDto
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        DateTime? ReminderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        DateTime? LastModificationDate { get; set; }

        TaskStatusHelper.TaskStatusType Status { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}
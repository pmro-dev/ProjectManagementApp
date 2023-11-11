using System.ComponentModel.DataAnnotations;
using Web.Common.Helpers;

namespace Web.Tasks.Create.Interfaces
{
    public interface ITaskCreateInputVM
    {
        string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        DateTime DueDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        DateTime? ReminderDate { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}
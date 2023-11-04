using Microsoft.AspNetCore.Mvc.Rendering;
using Project_DomainEntities.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface ITaskEditOutputVM
    {
		private const string DataFormat = "{0:yyyy-MM-dd}";

		string Description { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
		//[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
		DateTime DueDate { get; set; }
        int Id { get; set; }
        DateTime? ReminderDate { get; set; }
        TaskStatusHelper.TaskStatusType Status { get; set; }
        SelectList? StatusSelector { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        SelectList? TodoListsSelector { get; set; }
        string UserId { get; set; }
    }
}
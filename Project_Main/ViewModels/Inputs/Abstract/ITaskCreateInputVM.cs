using Project_DomainEntities.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Inputs.Abstract
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
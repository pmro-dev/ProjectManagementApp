using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using App.Features.TodoLists.Common.Models;

namespace App.Features.TodoLists.Common.Tags;

public interface ITodoListTagModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	Guid TodoListId { get; set; }

	[ForeignKey(nameof(TodoListId))]
	TodoListModel? TodoList { get; set; }

	[Required]
	Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	TagModel? Tag { get; set; }
}

using App.Features.Tags.Common.Models;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Features.TodoLists.Common.Tags;

public class TodoListTagModel : ITodoListTagModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
	public Guid TodoListId { get; set; } = Guid.Empty;

	[ForeignKey(nameof(TodoListId))]
	public TodoListModel? TodoList { get; set; }

	[Required]
	public Guid TagId { get; set; } = Guid.Empty;

	[ForeignKey(nameof(TagId))]
	public TagModel? Tag { get; set; }
}

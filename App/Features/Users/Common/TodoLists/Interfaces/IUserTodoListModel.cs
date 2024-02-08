using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.TodoLists.Interfaces;

public interface IUserTodoListModel
{
	[Key]
	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Key]
	[Required]
	public Guid TodoListId { get; set; }

	[Required]
	[ForeignKey(nameof(TodoListId))]
	public TodoListModel? TodoList { get; set; }
}

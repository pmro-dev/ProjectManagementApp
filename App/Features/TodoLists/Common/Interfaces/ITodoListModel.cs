using App.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListModel : IBasicModelWithTitle
{
	ICollection<TaskModel> Tasks { get; set; }

	[Required]
	string UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	UserModel? Owner { get; set; }

	bool Equals(object? obj);
	int GetHashCode();
	bool IsTheSame(ITodoListModel obj);
}
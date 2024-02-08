using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectTodolistModel
{
	[Key]
	[Required]
	string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }

	[Key]
	[Required]
	string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }
}

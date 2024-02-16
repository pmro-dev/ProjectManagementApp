#region USINGS
using App.Features.Projects.Common.Models;
using App.Features.TEMP.Teams.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
#endregion

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamModel
{
	[Key]
	[Required]
	Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Name { get; set; }

	[Required]
	string Description { get; set; }

	string? LiderId { get; set; }

	UserModel? Lider { get; set; }

	ICollection<UserTeamModel> TeamMembers { get; set; }

	ICollection<ProjectModel> Projects { get; set; }
	ICollection<ProjectTeamModel> TeamProjects { get; set; }

	ICollection<TodoListModel> TodoLists { get; set; }
}
using App.Features.Projects.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Teams.Models;
using App.Features.Users.Common.TodoLists.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Description { get; set; }

	public ICollection<UserModel> Members { get; set; }
	public ICollection<UserTeamModel> TeamMembers { get; set; }

	public ICollection<ProjectModel> Projects { get; set; }
	public ICollection<ProjectTeamModel> TeamProjects { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; }
	public ICollection<UserTodoListModel> UserTodoLists { get; set; }
}
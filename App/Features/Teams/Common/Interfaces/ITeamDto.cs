using App.Features.Projects.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamDto
{
	Guid Id { get; set; }

	byte[] RowVersion { get; set; }

	string Name { get; set; }

	string Description { get; set; }

	string LiderId { get; set; }

	UserModel? Lider { get; set; }

	ICollection<UserModel> Members { get; set; }
	//ICollection<UserTeamModel> TeamMembers { get; set; }

	ICollection<ProjectModel> Projects { get; set; }
	ICollection<ProjectTeamModel> TeamProjects { get; set; }

	ICollection<TodoListModel> TodoLists { get; set; }
}

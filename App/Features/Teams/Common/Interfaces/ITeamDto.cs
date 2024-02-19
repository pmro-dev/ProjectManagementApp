using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
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

	UserDto? Lider { get; set; }

	ICollection<UserDto> Members { get; set; }
	ICollection<UserTeamDto> TeamMembers { get; set; }

	ICollection<ProjectDto> Projects { get; set; }
	ICollection<ProjectTeamDto> TeamProjects { get; set; }

	ICollection<TodoListDto> TodoLists { get; set; }
}

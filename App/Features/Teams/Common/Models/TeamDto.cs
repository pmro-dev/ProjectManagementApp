using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Teams.Common.Models;

public class TeamDto : ITeamDto
{
	public Guid Id { get; set; }

	public byte[] RowVersion { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public string LiderId { get; set; }

	public virtual UserDto? Lider { get; set; }

	public ICollection<UserDto> Members { get; set; }
	public ICollection<UserTeamDto> TeamMembers { get; set; }

	public ICollection<ProjectDto> Projects { get; set; }
	public ICollection<ProjectTeamDto> TeamProjects { get; set; }

	public ICollection<TodoListDto> TodoLists { get; set; }

	public TeamDto(string name, string description)
	{
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Name = name;
		Description = description;
		LiderId = string.Empty;

		Members = new List<UserDto>();
		TeamMembers = new List<UserTeamDto>();
		Projects = new List<ProjectDto>();
		TeamProjects = new List<ProjectTeamDto>();
		TodoLists = new List<TodoListDto>();
	}
}

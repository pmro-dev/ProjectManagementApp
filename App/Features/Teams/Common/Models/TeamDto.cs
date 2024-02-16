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

	public virtual UserModel? Lider { get; set; }

	public ICollection<UserModel> Members { get; set; }
	//public ICollection<UserTeamModel> TeamMembers { get; set; }

	public ICollection<ProjectModel> Projects { get; set; }
	public ICollection<ProjectTeamModel> TeamProjects { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; }

	public TeamDto(string name, string description)
	{
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Name = name;
		Description = description;
		LiderId = string.Empty;

		Members = new List<UserModel>();
		//TeamMembers = new List<UserTeamModel>();
		Projects = new List<ProjectModel>();
		TeamProjects = new List<ProjectTeamModel>();
		TodoLists = new List<TodoListModel>();
	}
}

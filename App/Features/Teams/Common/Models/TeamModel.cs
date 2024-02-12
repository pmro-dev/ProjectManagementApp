#region USINGS
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Projects.Models;
using App.Features.Users.Common.Teams.Models;
using App.Features.Users.Common.TodoLists.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Teams.Common.Models;

public class TeamModel : ITeamModel
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public string LiderId { get; set; }

	[Required]
	[ForeignKey(nameof(LiderId))]
	public virtual UserModel? Lider { get; set; }

	public ICollection<UserModel> Members { get; set; }
	public ICollection<UserTeamModel> TeamMembers { get; set; }

	public ICollection<ProjectModel> Projects { get; set; }
	public ICollection<ProjectTeamModel> TeamProjects { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; }
	public ICollection<UserTodoListModel> UserTodoLists { get; set; }

	public TeamModel(string name, string description, string liderId)
	{
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Name = name;
		Description = description;
		LiderId = liderId;

		Members = new List<UserModel>();
		TeamMembers = new List<UserTeamModel>();
		Projects = new List<ProjectModel>();
		TeamProjects = new List<ProjectTeamModel>();
		TodoLists = new List<TodoListModel>();
		UserTodoLists = new List<UserTodoListModel>();
	}
}
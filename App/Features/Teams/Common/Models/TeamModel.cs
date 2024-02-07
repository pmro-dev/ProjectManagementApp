#region USINGS
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Teams.Common.Models;

public class TeamModel : ITeamModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public string LiderId { get; set; }

	[Required]
	[ForeignKey(nameof(LiderId))]
	public virtual UserModel? Lider { get; set; }

	public ICollection<UserModel> Members { get; set; } = new List<UserModel>();
	public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
	public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();

	public TeamModel(string name, string description, string liderId)
	{
		Id = Guid.NewGuid();
		Name = name;
		Description = description;
		LiderId = liderId;
	}
}
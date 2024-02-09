using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Projects.Models;
using App.Features.Users.Common.Roles.Models;
using App.Features.Users.Common.Teams.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Models.Interfaces;

public interface IUserModel : IEquatable<IUserModel>
{
	[Required]
	string DataVersion { get; set; }

	[Required]
	[DataType(DataType.EmailAddress)]
	string Email { get; set; }

	[Required]
	string FirstName { get; set; }

	[Required]
	string LastName { get; set; }

	[Required]
	public string CompanyName { get; set; }

	[Required]
	public string JobTitle { get; set; }

	[Required]
	[DataType(DataType.PhoneNumber)]
	public string Phone { get; set; }

	[Required]
	string NameIdentifier { get; set; }

	[Required]
	[DataType(DataType.Password)]
	string Password { get; set; }

	[Required]
	string Provider { get; set; }

	[Key]	
	[Required]
	string Id { get; set; }

	[Required]
	string Username { get; set; }

    ICollection<RoleModel> Roles { get; set; }
    ICollection<UserRoleModel> UserRoles { get; set; }

	ICollection<TeamModel> Teams { get; set; }
	ICollection<UserTeamModel> UserTeams { get; set; }

	ICollection<ProjectModel> Projects { get; set; }
	ICollection<UserProjectModel> ClientProjects { get; set; }

	ICollection<BudgetModel> Budgets { get; set; }
}
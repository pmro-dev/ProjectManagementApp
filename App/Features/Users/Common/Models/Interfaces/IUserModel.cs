using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Roles.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Models.Interfaces;

public interface IUserModel : IEquatable<IUserModel>
{
	[Required]
	string DataVersion { get; set; }

	[Required]
	[DataType(DataType.EmailAddress)]
	string Email { get; set; }
    string FirstName { get; set; }
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

	[Required]
	[Key]	
	string Id { get; set; }

	[Required]
	string Username { get; set; }

    ICollection<UserRoleModel> UserRoles { get; set; }
	ICollection<TeamModel> UserTeams { get; set; }
	ICollection<ProjectModel> UserProjects { get; set; }
	ICollection<BudgetModel> UserBudgets { get; set; }
}
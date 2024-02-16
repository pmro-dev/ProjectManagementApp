#region USINGS
using App.Features.Users.Common.Roles.Models;
using System.ComponentModel.DataAnnotations;
#endregion

namespace App.Features.Users.Common.Models.Interfaces;

public interface IUserModel : IEquatable<IUserModel>
{
	[Key]	
	[Required]
	string Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

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

	[Required]
	string Username { get; set; }

	//public ICollection<TodoListModel> CreatedTodoLists { get; set; }

	//public ICollection<TodoListModel> OwnedTodoLists { get; set; }

	public ICollection<RoleModel> Roles { get; set; }
	public ICollection<UserRoleModel> UserRoles { get; set; }

	//public ICollection<TeamModel> ManagedTeams { get; set; }

	//public ICollection<TeamModel> Teams { get; set; }
	//public ICollection<UserTeamModel> TeamMembers { get; set; }

	//public ICollection<ProjectModel> ManagedProjects { get; set; }

	//public ICollection<ProjectModel> OwnedProjects { get; set; }
	//public ICollection<UserProjectModel> ProjectClients { get; set; }

	//public ICollection<TaskModel> Tasks { get; set; }

	//public ICollection<BudgetModel> Budgets { get; set; }

	//public ICollection<BillingModel> Billings { get; set; }

	//public ICollection<IncomeModel> Incomes { get; set; }
}
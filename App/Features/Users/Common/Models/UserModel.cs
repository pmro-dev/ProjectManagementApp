﻿#region USINGS
using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Helpers;
using App.Features.Users.Common.Models.Interfaces;
using App.Features.Users.Common.Roles.Models;
using System.ComponentModel.DataAnnotations;
#endregion

namespace App.Features.Users.Common.Models;

public sealed class UserModel : IUserModel
{
	[Key]
	[Required]
	public string Id { get; set; } = Guid.NewGuid().ToString();
	[Required]
	// ConcurrencyStamp
	public string DataVersion { get; set; } = Guid.NewGuid().ToString();

	[Required]
	public string Provider { get; set; } = string.Empty;

	[Required]
	public string NameIdentifier { get; set; } = string.Empty;

	[Required]
	[MinLength(UserAttributesHelper.UsernameMinLength)]
	[MaxLength(UserAttributesHelper.UsernameMaxLength)]
	public string Username { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MinLength(UserAttributesHelper.FirstNameMinLength)]
	[MaxLength(UserAttributesHelper.FirstNameMaxLength)]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[MinLength(UserAttributesHelper.LastNameMinLength)]
	[MaxLength(UserAttributesHelper.LastNameMaxLength)]
	public string LastName { get; set; } = string.Empty;

	[Required]
	public string CompanyName { get; set; } = string.Empty;

	[Required]
	public string JobTitle { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.PhoneNumber)]
	public string Phone { get; set; } = string.Empty;

	public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
	public ICollection<TeamModel> UserTeams { get; set; } = new List<TeamModel>();
	public ICollection<ProjectModel> UserProjects { get; set; } = new List<ProjectModel>();
	public ICollection<BudgetModel> UserBudgets { get; set; } = new List<BudgetModel>();

	public override bool Equals(object? obj)
	{
		if (obj == null || !GetType().Equals(obj.GetType())) { return false; }
		else
		{
			var user = (IUserModel)obj;
			return NameIdentifier == user.NameIdentifier &&
				Provider == user.Provider &&
				Username == user.Username &&
				FirstName == user.FirstName &&
				LastName == user.LastName &&
				Email == user.Email;
		}
	}

	public bool Equals(IUserModel? other)
	{
		if (other == null || !GetType().Equals(other.GetType())) { return false; }
		else
		{
			return NameIdentifier == other.NameIdentifier &&
				Provider == other.Provider &&
				Username == other.Username &&
				FirstName == other.FirstName &&
				LastName == other.LastName &&
				Email == other.Email;
		}
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(NameIdentifier);
	}
}
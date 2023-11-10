using Domain.Helpers;
using Domain.Interfaces.ForIdentity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.ForIdentity;

public sealed class UserModel : IUserModel
{
	[Key]
	[Required]
	public string UserId { get; set; } = Guid.NewGuid().ToString();
	[Required]
	// ConcurrencyStamp
	public string DataVersion { get; set; } = Guid.NewGuid().ToString();

	[Required]
	public string Provider { get; set; } = string.Empty;

	[Required]
	public string NameIdentifier { get; set; } = string.Empty;

	[Required]
	[MinLength(AttributesHelper.UsernameMinLength)]
	[MaxLength(AttributesHelper.UsernameMaxLength)]
	public string Username { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MinLength(AttributesHelper.FirstNameMinLength)]
	[MaxLength(AttributesHelper.FirstNameMaxLength)]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[MinLength(AttributesHelper.LastNameMinLength)]
	[MaxLength(AttributesHelper.LastNameMaxLength)]
	public string LastName { get; set; } = string.Empty;

	public ICollection<IUserRoleModel> UserRoles { get; set; } = new List<IUserRoleModel>();

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
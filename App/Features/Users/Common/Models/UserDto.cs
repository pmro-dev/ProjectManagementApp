using App.Features.Users.Common.Models.Interfaces;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Models;

public class UserDto : IUserDto
{
    public UserDto() { NameIdentifier = Id; }

    public string Id { get; set; } = Guid.NewGuid().ToString();
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };
	public string Provider { get; set; } = string.Empty;
	public string NameIdentifier { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string CompanyName { get; set; } = string.Empty;
	public string JobTitle { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
	public ICollection<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
}

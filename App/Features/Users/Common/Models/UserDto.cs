using App.Features.Users.Common.Roles;
using App.Features.Users.Interfaces;

namespace App.Features.Users.Common.Models;

public class UserDto : IUserDto
{
    public UserDto()
    {
		NameIdentifier = UserId;
	}

    public string UserId { get; set; } = Guid.NewGuid().ToString();
	public string DataVersion { get; set; } = Guid.NewGuid().ToString();
	public string Provider { get; set; } = string.Empty;
	public string NameIdentifier { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public ICollection<IUserRoleDto> UserRoles { get; set; } = new List<IUserRoleDto>();
}

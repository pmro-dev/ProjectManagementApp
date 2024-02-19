using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Models.Interfaces;

public interface IUserDto
{
	byte[] RowVersion { get; set; }
    string Id { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string NameIdentifier { get; set; }
    string Password { get; set; }
    string Provider { get; set; }
    string Username { get; set; }
	string CompanyName { get; set; }
	string JobTitle { get; set; }
	string Phone { get; set; }
	ICollection<RoleDto> Roles { get; set; }
	ICollection<UserRoleDto> UserRoles { get; set; }
}
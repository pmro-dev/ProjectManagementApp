using Project_IdentityDomainEntities;

namespace Project_Main.Infrastructure.DTOs
{
    public interface IUserDto
    {
        string DataVersion { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string NameIdentifier { get; set; }
        string Password { get; set; }
        string Provider { get; set; }
        string UserId { get; set; }
        string Username { get; set; }
        List<UserRoleModel> UserRoles { get; set; }
    }
}
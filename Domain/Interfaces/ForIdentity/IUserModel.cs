namespace Domain.Interfaces.ForIdentity
{
    public interface IUserModel : IEquatable<IUserModel>
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
        ICollection<IUserRoleModel> UserRoles { get; set; }
    }
}
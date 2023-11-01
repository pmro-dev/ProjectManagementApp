namespace Project_IdentityDomainEntities
{
    public interface IUserModel
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

        bool Equals(object? obj);
        bool Equals(UserModel? other);
        int GetHashCode();
    }
}
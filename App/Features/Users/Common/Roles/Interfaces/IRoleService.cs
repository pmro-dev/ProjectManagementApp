using App.Features.Users.Common.Models;

namespace App.Features.Users.Common.Roles.Interfaces
{
    public interface IRoleService
    {
        Task AddDefaultRoleToUserAsync(UserDto userDto);
    }
}
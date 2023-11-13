using App.Common.Interfaces;
using App.Features.Users.Common.Models;

namespace App.Features.Users.Interfaces;

public interface IUserFactory : IBaseEntityFactory<UserModel, UserDto>
{
}
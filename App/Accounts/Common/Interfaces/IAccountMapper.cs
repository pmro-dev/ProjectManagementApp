using Web.Accounts.Login;
using Web.Accounts.Login.Interfaces;
using Web.Accounts.Register;
using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Common.Interfaces;

public interface IAccountMapper
{
    ILoginInputDto TransferToDto(LoginInputVM loginInputVM);
    IUserDto TransferToUserDto(RegisterInputVM registerInputVM);
    IUserDto TransferToUserDto(IUserModel userModel);
    IUserModel TransferToUserModel(ILoginInputDto loginInputDto);
    IUserModel TransferToModel(IUserDto userDto);
}
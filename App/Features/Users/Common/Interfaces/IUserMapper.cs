using App.Features.Users.Interfaces;
using App.Features.Users.Login;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Register;

namespace App.Features.Users.Common.Interfaces;

public interface IUserMapper
{
	ILoginInputDto TransferToDto(LoginInputVM loginInputVM);
	IUserDto TransferToUserDto(RegisterInputVM registerInputVM);
	IUserDto TransferToUserDto(IUserModel userModel);
	IUserModel TransferToUserModel(ILoginInputDto loginInputDto);
	IUserModel TransferToModel(IUserDto userDto);
}
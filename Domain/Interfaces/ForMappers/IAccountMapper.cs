namespace Application.Interfaces;

public interface IAccountMapper
{
    ILoginInputDto TransferToDto(LoginInputVM loginInputVM);
    IUserDto TransferToUserDto(RegisterInputVM registerInputVM);
    IUserDto TransferToUserDto(IUserModel userModel);
    IUserModel TransferToUserModel(ILoginInputDto loginInputDto);
    IUserModel TransferToModel(IUserDto userDto);
}
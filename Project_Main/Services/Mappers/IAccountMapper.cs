using Project_IdentityDomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.Inputs.ViewModels;

namespace Project_Main.Services.DTO
{
    public interface IAccountMapper
    {
        ILoginInputDto TransferToDto(LoginInputVM loginInputVM);
        IUserDto TransferToUserDto(RegisterInputVM registerInputVM);
        IUserDto TransferToUserDto(IUserModel userModel);
        IUserModel TransferToUserModel(ILoginInputDto loginInputDto);
        IUserModel TransferToModel(IUserDto userDto);
    }
}
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Models.ViewModels.InputModels;

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
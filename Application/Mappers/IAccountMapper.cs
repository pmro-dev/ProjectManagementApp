using Application.DTOs.Entities;
using Application.DTOs.ViewModels.Inputs.Abstract;
using Domain.Interfaces.ForIdentity;

namespace Application.Mappers;

public interface IAccountMapper
{
    ILoginInputDto TransferToDto(LoginInputVM loginInputVM);
    IUserDto TransferToUserDto(RegisterInputVM registerInputVM);
    IUserDto TransferToUserDto(IUserModel userModel);
    IUserModel TransferToUserModel(ILoginInputDto loginInputDto);
    IUserModel TransferToModel(IUserDto userDto);
}
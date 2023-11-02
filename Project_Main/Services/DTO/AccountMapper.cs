using Project_IdentityDomainEntities;
//using Project_Main.Infrastructure.DTOs;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO
{
    public class AccountMapper : IAccountMapper
    {
        public ILoginInputDto TransferToDto(LoginInputVM loginInputVM)
        {
            return new LoginInputDto
            {
                Username = loginInputVM.Name,
                Password = loginInputVM.Password
            };
        }

        public IUserDto TransferToUserDto(RegisterInputVM registerInputVM)
        {
            return new UserDto
            {
                UserId = string.Empty,
                DataVersion = string.Empty,
                Provider = string.Empty,
                NameIdentifier = string.Empty,
                Username = registerInputVM.Name,
                Password = registerInputVM.Password,
                Email = registerInputVM.Email,
                FirstName = registerInputVM.Name,
                LastName = registerInputVM.Name,
                UserRoles = new List<UserRoleModel> { }
            };
        }

        public IUserDto TransferToUserDto(IUserModel userModel)
        {
            return new UserDto
            {
                UserId = userModel.UserId,
                DataVersion = userModel.DataVersion,
                Provider = userModel.Provider,
                NameIdentifier = userModel.NameIdentifier,
                Username = userModel.Username,
                Password = userModel.Password,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserRoles = userModel.UserRoles
            };
        }

        public IUserModel TransferToModel(IUserDto userDto)
        {
            UserModel newUser = new()
            {
                NameIdentifier = string.Empty,
                Username = userDto.Username,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Provider = userDto.Provider,
                UserRoles = userDto.UserRoles
            };

            newUser.NameIdentifier = newUser.UserId;
            newUser.UserRoles.ForEach(userRole => userRole.User = newUser);

            return newUser;
        }

        public IUserModel TransferToUserModel(ILoginInputDto loginInputDto)
        {
            UserModel loginUser = new()
            {
                Username = loginInputDto.Username,
                Password = loginInputDto.Password,
            };

            return loginUser;
        }
    }
}

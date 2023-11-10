using Application.DTOs.Entities;

namespace Application.Services.Identity;

public interface IUserRegisterService
{
    Task<bool> RegisterAsync(IUserDto userDto);
}
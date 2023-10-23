﻿using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Services.Identity
{
    public interface IUserRegisterService
    {
        Task<bool> RegisterUserAsync(UserDto userDto);
    }
}
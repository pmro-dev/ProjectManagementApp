﻿using App.Common.Helpers;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles;
using App.Features.Users.Interfaces;
using App.Infrastructure.Databases.Common.Helpers;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace App.Features.Users.Common;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserFactory _userFactory;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IHttpContextAccessor httpContextAccessor, IIdentityUnitOfWork identityUnitOfWork,
        ILogger<UserService> logger, IUserFactory userFactory, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityUnitOfWork = identityUnitOfWork;
        _userRepository = _identityUnitOfWork.UserRepository;
        _roleRepository = _identityUnitOfWork.RoleRepository;
        _logger = logger;
        _userFactory = userFactory;
        _mapper = mapper;
    }

    public async Task AddUserAsync(IUserDto userDto)
    {
        IRoleModel? roleModel = await _roleRepository.GetByFilterAsync(r => r.Name == IdentitySeedData.DefaultRole);

        if (roleModel is null)
        {
            //TODO add logging
            throw new InvalidOperationException("Role model was not found");
        }

        IRoleDto? roleDto = _mapper.Map<IRoleDto>(roleModel);

        var userRoleDto = _userFactory.CreateUserRoleDto();
        userRoleDto.UserId = userDto.UserId;
        userRoleDto.RoleId = roleDto.Id;
        
        userDto.UserRoles.Add(userRoleDto);
        
        IUserModel userModel = _mapper.Map<IUserModel>(userDto);

        await _userRepository.AddAsync((UserModel)userModel);
    }

    public string GetSignedInUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

        return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Error with signed In User");
    }

    public async Task UpdateUserAsync(IUserDto userBasedOnProviderClaimsDto, Claim authSchemeClaimWithProviderName)
    {
        IUserModel? userFromDb = await _userRepository.GetAsync(userBasedOnProviderClaimsDto.NameIdentifier);

        if (userFromDb is null)
        {
            _logger?.LogCritical(MessagesPacket.LogEntityNotFoundInDbSet, nameof(UpdateUserAsync), userBasedOnProviderClaimsDto.NameIdentifier, HelperDatabase.UsersDbSetName);
            throw new InvalidOperationException(MessagesPacket.EntityNotFoundByIdInDb(nameof(UpdateUserAsync), HelperDatabase.UsersDbSetName, -2));
        }

        bool doesUserUseOtherProvider = userBasedOnProviderClaimsDto.Provider != CookieAuthenticationDefaults.AuthenticationScheme;

        if (doesUserUseOtherProvider && !userBasedOnProviderClaimsDto.Equals(userFromDb))
        {
            userFromDb.FirstName = userBasedOnProviderClaimsDto.FirstName;
            userFromDb.LastName = userBasedOnProviderClaimsDto.LastName;
            userFromDb.Username = userBasedOnProviderClaimsDto.Username;
            userFromDb.Provider = authSchemeClaimWithProviderName.Value;
            userFromDb.Email = userBasedOnProviderClaimsDto.Email;

            _userRepository.Update((UserModel)userFromDb);
        }
    }

    public async Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity principle)
    {
        IEnumerable<IRoleModel> userRoles = await _userRepository.GetRolesAsync(userId);
        IEnumerable<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

        foreach (string roleName in userRolesNames)
        {
            principle?.AddClaim(new Claim(ClaimTypes.Role, roleName));
        }
	}
}
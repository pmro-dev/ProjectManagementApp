﻿using App.Features.Exceptions.Throw;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Interfaces;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace App.Features.Users.Common;

public class UserService : IUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IRoleService _roleService;
	private readonly IMapper _mapper;
	private readonly ILogger<UserService> _logger;

	public UserService(IHttpContextAccessor httpContextAccessor, IIdentityUnitOfWork identityUnitOfWork,
		ILogger<UserService> logger, IMapper mapper, IRoleService roleService)
	{
		_httpContextAccessor = httpContextAccessor;
		_identityUnitOfWork = identityUnitOfWork;
		_userRepository = _identityUnitOfWork.UserRepository;
		_logger = logger;
		_mapper = mapper;
		_roleService = roleService;
	}

	public async Task AddNewUserAsync(UserDto userDto)
	{
		await _roleService.AddDefaultRoleToUserAsync(userDto);

		UserModel userModel = _mapper.Map<UserModel>(userDto);

		await _userRepository.AddAsync(userModel);
	}

	public string GetSignedInUserId()
	{
		return GetSignedInUser().FindFirstValue(ClaimTypes.NameIdentifier);
	}

	public ClaimsPrincipal GetSignedInUser()
	{
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext is null)
		{
			_logger.LogCritical(ExceptionsMessages.LogHttpContextObjectIsNull, nameof(GetSignedInUser));
			throw new ArgumentException(ExceptionsMessages.HttpContextObjectIsNull(nameof(GetSignedInUser)));
		}

		ClaimsPrincipal? user = httpContext.User;
		ExceptionsService.WhenPrincipalIsNullThrow(nameof(GetSignedInUser), user, _logger);

		return user;
	}

	public async Task UpdateUserModelAsync(UserDto userBasedOnProviderDataDto, Claim authenticationSchemeClaim)
	{
		UserModel? userFromDb = await _userRepository.GetAsync(userBasedOnProviderDataDto.NameIdentifier);
		ExceptionsService.WhenEntityIsNullThrow(nameof(UpdateUserModelAsync), userFromDb, _logger, userBasedOnProviderDataDto.NameIdentifier);

		bool IsUserUsedExternalAuthProvider = userBasedOnProviderDataDto.Provider != CookieAuthenticationDefaults.AuthenticationScheme;
		bool IsUserDataNotTheSame = !userBasedOnProviderDataDto.Equals(userFromDb);

		if (IsUserUsedExternalAuthProvider && IsUserDataNotTheSame)
			SetNewDataForUser(userFromDb, userBasedOnProviderDataDto, authenticationSchemeClaim.Value);
	}

	private void SetNewDataForUser(UserModel? userFromDb, UserDto? userBasedOnProviderDataDto, string providerName)
	{
		ExceptionsService.WhenEntityIsNullThrow(nameof(UpdateUserModelAsync), userFromDb, _logger);
		ExceptionsService.WhenEntityIsNullThrow(nameof(UpdateUserModelAsync), userBasedOnProviderDataDto, _logger);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(UpdateUserModelAsync), providerName, nameof(providerName), _logger);

		userFromDb!.FirstName = userBasedOnProviderDataDto!.FirstName;
		userFromDb.LastName = userBasedOnProviderDataDto.LastName;
		userFromDb.Username = userBasedOnProviderDataDto.Username;
		userFromDb.Provider = providerName;
		userFromDb.Email = userBasedOnProviderDataDto.Email;

		_userRepository.Update(userFromDb);
	}

	public async Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity? identity)
	{
		ExceptionsService.WhenArgumentIsInvalidThrow(nameof(SetRolesForUserPrincipleAsync), userId, nameof(userId), _logger);
		ExceptionsService.WhenIdentityIsNullThrow(identity, _logger);

		IEnumerable<RoleModel> userRoles = await _userRepository.GetRolesAsync(userId);
		IEnumerable<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

		foreach (string roleName in userRolesNames)
		{
			identity!.AddClaim(new Claim(ClaimTypes.Role, roleName));
		}
	}
}

using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Interfaces;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using App.Infrastructure.Helpers;
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
		var httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

		return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Error with signed In User");
	}

	public async Task UpdateUserInDbAsync(UserDto userBasedOnProviderDataDto, Claim authenticationSchemeClaim)
	{
		UserModel? userFromDb = await _userRepository.GetAsync(userBasedOnProviderDataDto.NameIdentifier);

		if (userFromDb is null)
			ExceptionsService.ThrowCriticalEntityNotFoundInDb(nameof(UpdateUserInDbAsync), nameof(UserModel), userBasedOnProviderDataDto.NameIdentifier, _logger);

		bool IsUserUsedExternalAuthProvider = userBasedOnProviderDataDto.Provider != CookieAuthenticationDefaults.AuthenticationScheme;
		bool IsNotUserDataTheSame = !userBasedOnProviderDataDto.Equals(userFromDb);

		if (IsUserUsedExternalAuthProvider && IsNotUserDataTheSame)
			SetNewDataForUser(userFromDb, userBasedOnProviderDataDto, authenticationSchemeClaim.Value);
	}

	private void SetNewDataForUser(UserModel? userFromDb, UserDto? userBasedOnProviderDataDto, string providerName)
	{
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(UpdateUserInDbAsync), userFromDb, _logger);
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(UpdateUserInDbAsync), userBasedOnProviderDataDto, _logger);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(UpdateUserInDbAsync), providerName, nameof(providerName), _logger);

		userFromDb!.FirstName = userBasedOnProviderDataDto!.FirstName;
		userFromDb.LastName = userBasedOnProviderDataDto.LastName;
		userFromDb.Username = userBasedOnProviderDataDto.Username;
		userFromDb.Provider = providerName;
		userFromDb.Email = userBasedOnProviderDataDto.Email;

		_userRepository.Update(userFromDb);
	}

	public async Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity? identity)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(SetRolesForUserPrincipleAsync), userId, nameof(userId), _logger);
		ExceptionsService.WhenIdentityIsNullThrowCritical(identity, _logger);

		IEnumerable<RoleModel> userRoles = await _userRepository.GetRolesAsync(userId);
		IEnumerable<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

		foreach (string roleName in userRolesNames)
		{
			identity!.AddClaim(new Claim(ClaimTypes.Role, roleName));
		}
	}
}

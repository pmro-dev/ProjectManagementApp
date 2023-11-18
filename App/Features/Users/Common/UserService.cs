using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles;
using App.Features.Users.Interfaces;
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

	public async Task AddNewUserToDbAsync(UserDto userDto)
    {
		RoleModel? roleModel = await _roleRepository.GetByFilterAsync(r => r.Name == IdentitySeedData.DefaultRole);
		ExceptionsService.ThrowWhenRoleNotFoundInDb(nameof(AddNewUserToDbAsync), roleModel, IdentitySeedData.DefaultRole, _logger);

		RoleDto? roleDto = _mapper.Map<RoleDto>(roleModel);

        var userRoleDto = _userFactory.CreateUserRoleDto();
        userRoleDto.UserId = userDto.UserId;
        userRoleDto.RoleId = roleDto.Id;
        userDto.UserRoles.Add(userRoleDto);
        
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
        {
            _logger?.LogCritical(MessagesPacket.LogEntityNotFoundInDbSet, nameof(UpdateUserAsync), nameof(UserModel), userBasedOnProviderClaimsDto.NameIdentifier);
            throw new InvalidOperationException(MessagesPacket.EntityNotFoundInDb(nameof(UpdateUserAsync), nameof(UserModel), userBasedOnProviderClaimsDto.NameIdentifier));
        }

        bool doesUserUseOtherProvider = userBasedOnProviderClaimsDto.Provider != CookieAuthenticationDefaults.AuthenticationScheme;

        if (doesUserUseOtherProvider && !userBasedOnProviderClaimsDto.Equals(userFromDb))
        {
            userFromDb.FirstName = userBasedOnProviderClaimsDto.FirstName;
            userFromDb.LastName = userBasedOnProviderClaimsDto.LastName;
            userFromDb.Username = userBasedOnProviderClaimsDto.Username;
            userFromDb.Provider = authSchemeClaimWithProviderName.Value;
            userFromDb.Email = userBasedOnProviderClaimsDto.Email;

            _userRepository.Update(userFromDb);
        }
    }

	public async Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity? identity)
    {
        ExceptionsService.ThrowWhenArgumentIsInvalid(nameof(SetRolesForUserPrincipleAsync), userId, nameof(userId), _logger);
		ExceptionsService.ThrowWhenIdentityIsNull(identity, _logger);

		IEnumerable<RoleModel> userRoles = await _userRepository.GetRolesAsync(userId);
        IEnumerable<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

        foreach (string roleName in userRolesNames)
        {
			identity!.AddClaim(new Claim(ClaimTypes.Role, roleName));
        }
	}
}

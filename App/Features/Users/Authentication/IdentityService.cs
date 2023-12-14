using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using System.Security.Claims;
namespace App.Features.Users.Authentication;

public class IdentityService : IIdentityService
{
    private readonly IUserFactory _userFactory;
    private readonly ILogger<IdentityService>  _logger;

	public IdentityService(IUserFactory userFactory, ILogger<IdentityService> logger)
	{
		_userFactory = userFactory;
		_logger = logger;
	}

	public UserDto CreateUser(ClaimsIdentity? identity, Claim authSchemeClaimWithProviderName)
    {
		ExceptionsService.WhenIdentityIsNullThrow(identity, _logger);

		identity!.AddClaim(authSchemeClaimWithProviderName);
        var identityClaims = identity.Claims;

		UserDto userBasedOnClaims = _userFactory.CreateDto();
        userBasedOnClaims.NameIdentifier = identityClaims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        userBasedOnClaims.UserId = identityClaims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        userBasedOnClaims.Username = identityClaims.Single(c => c.Type == ClaimTypes.GivenName).Value;
        userBasedOnClaims.Provider = authSchemeClaimWithProviderName.Value;
        userBasedOnClaims.Email = identityClaims.Single(c => c.Type == ClaimTypes.Email).Value;

        return userBasedOnClaims;
    }
}

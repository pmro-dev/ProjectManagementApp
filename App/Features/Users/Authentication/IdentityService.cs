using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Models;
using System.Security.Claims;
namespace App.Features.Users.Authentication;

public class IdentityService : IIdentityService
{
    private readonly IUserFactory _userFactory;

    public IdentityService(IUserFactory userFactory)
    {
        _userFactory = userFactory;
    }

    public IUserDto CreateUser(IEnumerable<Claim> identityClaims, Claim authSchemeClaimWithProviderName)
    {
		IUserDto userBasedOnClaims = _userFactory.CreateDto();

        userBasedOnClaims.NameIdentifier = identityClaims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        userBasedOnClaims.UserId = identityClaims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        userBasedOnClaims.Username = identityClaims.Single(c => c.Type == ClaimTypes.GivenName).Value;
        userBasedOnClaims.Provider = authSchemeClaimWithProviderName.Value;
        userBasedOnClaims.Email = identityClaims.Single(c => c.Type == ClaimTypes.Email).Value;

        return userBasedOnClaims;
    }
}

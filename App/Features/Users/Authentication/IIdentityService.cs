using App.Features.Users.Interfaces;
using System.Security.Claims;

namespace App.Features.Users.Authentication;

public interface IIdentityService
{
    IUserDto CreateUser(IEnumerable<Claim> identityClaims, Claim authSchemeClaimWithProviderName);
}
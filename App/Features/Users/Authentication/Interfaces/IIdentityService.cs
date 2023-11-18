using App.Features.Users.Common.Models;
using System.Security.Claims;

namespace App.Features.Users.Authentication.Interfaces;

public interface IIdentityService
{
	UserDto CreateUser(ClaimsIdentity? identity, Claim authSchemeClaimWithProviderName);
}
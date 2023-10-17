using Project_IdentityDomainEntities;
using System.Security.Claims;

namespace Project_Main.Services
{
	public interface IClaimsService
	{
		public ClaimsPrincipal CreateUserClaimsPrincipal(UserModel user);
	}
}

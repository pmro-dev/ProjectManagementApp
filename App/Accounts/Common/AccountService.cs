using System.Security.Claims;
using Web.Accounts.Common.Interfaces;

namespace Web.Accounts.Common;

public class AccountService : IAccountService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AccountService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetSignedInUserId()
	{
		var httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

		//return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Error with signed In User");
		return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Error with signed In User");
	}
}

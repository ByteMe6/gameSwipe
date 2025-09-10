using System.Security.Claims;

using GameSwipe.Application.Interfaces.Services;
using GameSwipe.Application.Services;

namespace GameSwipe.Api.Services;

public class IdentityService : IIdentityService
{
	private readonly IHttpContextAccessor _acc;

	public IdentityService(IHttpContextAccessor acc)
	{
		_acc = acc;
	}

	public long Id
	{
		get
		{
			var id = _acc.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User id not found in token");
			return long.Parse(id);
		}
	}

	public string Email
	{
		get
		{
			var email = _acc.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? throw new Exception("User email not found in token");
			return email;
		}
	}

	public string Username
	{
		get
		{
			var username = _acc.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? throw new Exception("User username not found in token");
			return username;
		}
	}

	public Roles Role
	{
		get
		{
			var role = _acc.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? throw new Exception("User role not found in token");
			return (Roles)Enum.Parse(typeof(Roles), role);
		}
	}

	public bool IsAdmin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.Owner;
		}
	}
}

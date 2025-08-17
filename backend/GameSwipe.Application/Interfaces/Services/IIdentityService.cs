using GameSwipe.Application.Services;

namespace GameSwipe.Application.Interfaces.Services;

public interface IIdentityService
{
	public long Id { get; }
	public string Email { get; }
	public string Username { get; }
	public Roles Role { get; }
	public bool IsAdmin { get; }
}

using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Interfaces.Services;

public interface ITokenService
{
	public string GenerateToken(User user);

	public DateTime GetIssueDate(string token);

	public long GetId(string token);

	public bool IsValid(string token, bool checkLifetime = false);
}

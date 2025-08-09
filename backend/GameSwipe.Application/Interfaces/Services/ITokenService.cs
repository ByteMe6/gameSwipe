using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Interfaces.Services;

public interface ITokenService
{
	public string GenerateToken(User user);

	public string RefreshToken(string token);
}

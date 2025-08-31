using GameSwipe.Application.Dtos.General;
using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Dtos.Users;

public class UserGetShortDto : GetShortDto
{
	public string Username { get; set; } = string.Empty;
	public string? Avatar { get; set; } = null;

	public UserGetShortDto(long id, string username, string name, string? avatar) : base(id, name)
	{
		Username = username;
		Avatar = avatar;
	}

	public static explicit operator UserGetShortDto(User user) => new(user.Id, user.Username, user.Name, user.Avatar);
}

using GameSwipe.Application.Dtos.General;

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
}

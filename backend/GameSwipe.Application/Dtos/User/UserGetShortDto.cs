namespace GameSwipe.Application.Dtos.User;

public class UserGetShortDto
{
	public long Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Avatar { get; set; } = null;
}

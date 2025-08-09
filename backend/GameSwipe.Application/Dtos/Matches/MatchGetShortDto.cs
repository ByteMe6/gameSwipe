using GameSwipe.Application.Dtos.Users;

namespace GameSwipe.Application.Dtos.Matches;

public class MatchGetShortDto
{
	public long Id { get; set; }
	public UserGetShortDto User { get; set; } = new();
	public UserGetShortDto TargetUser { get; set; } = new();
}

using GameSwipe.Application.Dtos.User;

namespace GameSwipe.Application.Dtos.Match;

public class MatchGetShortDto
{
	public long Id { get; set; }
	public UserGetShortDto User { get; set; } = new();
	public UserGetShortDto TargetUser { get; set; } = new();
}

using GameSwipe.Application.Dtos.Users;

namespace GameSwipe.Application.Dtos.Matches;

public class MatchGetShortDto
{
	public long Id { get; set; }
	public UserGetShortDto User { get; set; } = null!;
	public UserGetShortDto TargetUser { get; set; } = null!;

	public MatchGetShortDto(long id, UserGetShortDto user, UserGetShortDto targetUser)
	{
		Id = id;
		User = user;
		TargetUser = targetUser;
	}
}

using System.ComponentModel.DataAnnotations;

using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Dtos.Match;

public class MatchWriteDto
{
	public long? Id { get; set; }

	[Required]
	public long UserId { get; set; }
	[Required]
	public long TargetUserId { get; set; }
	[Required]
	public MatchStatus Status { get; set; }

	public MatchWriteDto(long userId, long targetUserId, MatchStatus status, long? id = null)
	{
		Id = id;
		UserId = userId;
		TargetUserId = targetUserId;
		Status = status;
	}
}

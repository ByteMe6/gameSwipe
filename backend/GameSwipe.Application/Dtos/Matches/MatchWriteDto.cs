using System.ComponentModel.DataAnnotations;

using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Dtos.Matches;

public class MatchWriteDto
{
	public long? Id { get; set; }

	public long? TargetUserId { get; set; }
	[Required]
	public MatchStatus Status { get; set; }

	public MatchWriteDto(long targetUserId, MatchStatus status, long? id = null)
	{
		Id = id;
		TargetUserId = targetUserId;
		Status = status;
	}
}

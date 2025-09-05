using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.GameRecords;

public class GameRecordWriteDto
{
	public long UserId { get; set; }
	[Required]
	public long GameId { get; set; }
	[Required]
	public int Playtime { get; set; }
	public string? Progress { get; set; }

	public GameRecordWriteDto(long userId, long gameId, int playtime, string? progress)
	{
		UserId = userId;
		GameId = gameId;
		Playtime = playtime;
		Progress = progress;
	}
}

using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.GameRecord;

public class GameRecordWriteDto
{
	public long? Id { get; set; }
	public long UserId { get; set; }
	[Required]
	public long GameId { get; set; }
	[Required]
	public int Playtime { get; set; }
	public string? Progress { get; set; }
}

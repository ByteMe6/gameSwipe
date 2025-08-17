using GameSwipe.Application.Dtos.Games;

namespace GameSwipe.Application.Dtos.GameRecords;

public class GameRecordGetShortDto
{
	public long Id { get; set; }
	public long PlayerId { get; set; }

	public GameGetShortDto Game { get; set; } = null!;

	public int Playtime { get; set; }
	public string Progress { get; set; } = string.Empty;
}

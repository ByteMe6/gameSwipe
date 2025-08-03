using GameSwipe.Application.Dtos.Game;

namespace GameSwipe.Application.Dtos.GameRecord;

public class GameRecordGetShortDto
{
	public long Id { get; set; }
	public long PlayerId { get; set; }

	public GameGetShortDto Game { get; set; } = new();

	public int Playtime { get; set; }
	public string Progress { get; set; } = string.Empty;
}

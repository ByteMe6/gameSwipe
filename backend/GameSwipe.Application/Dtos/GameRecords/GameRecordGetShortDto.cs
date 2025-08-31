using GameSwipe.Application.Dtos.Games;
using GameSwipe.DataAccess.Entities.Games;

namespace GameSwipe.Application.Dtos.GameRecords;

public class GameRecordGetShortDto
{
	public long Id { get; set; }
	public long PlayerId { get; set; }

	public GameGetShortDto Game { get; set; } = null!;

	public int Playtime { get; set; }
	public string Progress { get; set; } = string.Empty;

	public static explicit operator GameRecordGetShortDto(GameRecord ent) =>
		new()
		{
			Id = ent.Id,
			PlayerId = ent.User.Id,
			Game = (GameGetShortDto)ent.Game,
			Playtime = ent.Playtime,
			Progress = ent.Progress ?? ""
		};
}

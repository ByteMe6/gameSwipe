using GameSwipe.Application.Dtos.General;
using GameSwipe.DataAccess.Entities.Games;

namespace GameSwipe.Application.Dtos.Games;

public class GameGetShortDto(long id, string name) : GetShortDto(id, name)
{
	public static explicit operator GameGetShortDto(Game game) => new(game.Id, game.Name);
}

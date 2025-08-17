using GameSwipe.Application.Dtos.General;

namespace GameSwipe.Application.Dtos.Games;

public class GameGetShortDto(long id, string name) : GetShortDto(id, name)
{
}

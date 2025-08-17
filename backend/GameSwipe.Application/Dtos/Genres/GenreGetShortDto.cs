using GameSwipe.Application.Dtos.General;

namespace GameSwipe.Application.Dtos.Genres;

public class GenreGetShortDto(long id, string name) : GetShortDto(id, name)
{
}

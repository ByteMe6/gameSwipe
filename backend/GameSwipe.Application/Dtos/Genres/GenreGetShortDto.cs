using GameSwipe.Application.Dtos.General;
using GameSwipe.DataAccess.Entities.Games;

namespace GameSwipe.Application.Dtos.Genres;

public class GenreGetShortDto(long id, string name) : GetShortDto(id, name)
{
	public static explicit operator GenreGetShortDto(Genre genre) => new(genre.Id, genre.Name);
}

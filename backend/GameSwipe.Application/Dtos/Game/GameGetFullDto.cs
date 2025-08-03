using GameSwipe.Application.Dtos.Genre;

namespace GameSwipe.Application.Dtos.Game;

public class GameGetFullDto
{
	public long Id { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public List<GenreGetShortDto> Genres { get; set; } = [];
}

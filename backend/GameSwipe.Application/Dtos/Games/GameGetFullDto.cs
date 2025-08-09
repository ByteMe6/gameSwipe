using GameSwipe.Application.Dtos.Genres;

namespace GameSwipe.Application.Dtos.Games;

public class GameGetFullDto
{
	public long Id { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public List<GenreGetShortDto> Genres { get; set; } = [];
}

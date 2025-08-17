using GameSwipe.Application.Dtos.Genres;
using GameSwipe.DataAccess.Entities.Games;

namespace GameSwipe.Application.Interfaces.Services.Games;

public interface IGenreService
{
	public Task<GenreGetShortDto> GetGenreShortAsync(long id);

	public Task<Genre> GetGenreBareAsync(long id);

	public Task<bool> CreateGenreAsync(GenreWriteDto dto);

	public Task<bool> UpdateGenreAsync(GenreWriteDto dto);

	public Task<bool> DeleteGenreAsync(long id);

	public Task<List<GenreGetShortDto>> GetGenresAsync(long id);
}

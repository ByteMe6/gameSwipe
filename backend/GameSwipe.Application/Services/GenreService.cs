

using GameSwipe.Application.Dtos.Genres;
using GameSwipe.Application.Interfaces.Services.Games;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Games;

using Microsoft.EntityFrameworkCore;

namespace GameSwipe.Application.Services;

public class GenreService : IGenreService
{
	private readonly GameSwipeDbContext _db;
	public GenreService(GameSwipeDbContext db)
	{
		_db = db;
	}

	public async Task<bool> CreateGenreAsync(GenreWriteDto dto)
	{
		Genre entity = new()
		{
			Created = DateTime.UtcNow,
			Description = dto.Description,
			Name = dto.Name,
			Updated = DateTime.UtcNow,
		};
		_db.Genres.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteGenreAsync(long id)
	{
		Genre? entity = await _db.Genres.FindAsync(id);
		if(entity is null)
			throw new Exception("Genre not found");
		_db.Genres.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<Genre> GetGenreBareAsync(long id)
	{
		return await _db.Genres.Include(x => x.Games).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Genre not found");
	}

	public async Task<List<GenreGetShortDto>> GetGenresAsync(long id)
	{
		return await _db.Genres.Select(x => new GenreGetShortDto(x.Id, x.Name)).ToListAsync();
	}

	public async Task<GenreGetShortDto> GetGenreShortAsync(long id)
	{
		Genre? entity = await _db.Genres.FindAsync(id);
		if(entity is null)
			throw new Exception("Genre not found");
		return new GenreGetShortDto(entity.Id, entity.Name);
	}

	public async Task<bool> UpdateGenreAsync(GenreWriteDto dto)
	{
		Genre? entity = await _db.Genres.FindAsync(dto.Id);
		if(entity is null)
			throw new Exception("Genre not found");
		entity.Description = dto.Description;
		entity.Name = dto.Name;
		entity.Updated = DateTime.UtcNow;
		return await _db.SaveChangesAsync() > 0;
	}
}


using GameSwipe.Application.Dtos.Games;
using GameSwipe.Application.Dtos.Genres;
using GameSwipe.Application.Interfaces.Services.Games;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Games;

using Microsoft.EntityFrameworkCore;

namespace GameSwipe.Application.Services;

public class GameService : IGameService
{
	private readonly UserDbContext _db;

	public GameService(UserDbContext db)
	{
		_db = db;
	}

	public async Task<bool> CreateGameAsync(GameWriteDto dto)
	{
		Game entity = new Game()
		{
			Created = DateTime.UtcNow,
			Description = dto.Description,
			Genres = await _db.Genres.Where(x => dto.Genres.Contains(x.Id)).ToListAsync(),
			Name = dto.Name,
			Updated = DateTime.UtcNow,
		};

		await _db.Games.AddAsync(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteGameAsync(long id)
	{
		Game? entity = await _db.Games.FindAsync(id);
		if(entity is null)
			throw new Exception("Game not found");

		_db.Games.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<GameGetFullDto> GetGameFullAsync(long id)
	{
		Game? entity = await _db.Games.Include(x => x.Genres).FirstOrDefaultAsync(x => x.Id == id);
		if(entity is null)
			throw new Exception("Game not found");
		GameGetFullDto dto = new()
		{
			Created = entity.Created,
			Description = entity.Description,
			Genres = entity.Genres.Select(x => new GenreGetShortDto(x.Id, x.Name)).ToList(),
			Id = entity.Id,
			Name = entity.Name,
			Updated = entity.Updated
		};

		return dto;
	}

	public async Task<GameGetShortDto> GetGameShortAsync(long id)
	{
		Game? entity = await _db.Games.FindAsync(id);
		if(entity is null)
			throw new Exception("Game not found");
		return new GameGetShortDto(entity.Id, entity.Name);
	}

	public async Task<bool> UpdateGameAsync(GameWriteDto dto)
	{
		Game? entity = await _db.Games.FindAsync(dto.Id);
		if(entity is null)
			throw new Exception("Game not found");

		entity.Description = dto.Description;
		entity.Genres = await _db.Genres.Where(x => dto.Genres.Contains(x.Id)).ToListAsync();
		entity.Name = dto.Name;
		entity.Updated = DateTime.UtcNow;
		return await _db.SaveChangesAsync() > 0;
	}
}

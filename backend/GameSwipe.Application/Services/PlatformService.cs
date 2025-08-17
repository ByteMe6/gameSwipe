
using GameSwipe.Application.Dtos.Platforms;
using GameSwipe.Application.Interfaces.Services.Others;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Other;

using Microsoft.EntityFrameworkCore;

namespace GameSwipe.Application.Services;

public class PlatformService : IPlatformService
{
	private readonly GameSwipeDbContext _db;

	public PlatformService(GameSwipeDbContext db)
	{
		_db = db;
	}

	public async Task<bool> CreatePlatformAsync(PlatformWriteDto dto)
	{
		Platform entity = new()
		{
			Created = DateTime.UtcNow,
			Description = dto.Description,
			Name = dto.Name,
			Updated = DateTime.UtcNow,
		};
		_db.Platforms.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeletePlatformAsync(long id)
	{
		Platform? entity = await _db.Platforms.FindAsync(id);
		if(entity == null)
			throw new Exception("Platform not found");

		_db.Platforms.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<Platform> GetPlatformBareAsync(long id)
	{
		return await _db.Platforms.FindAsync(id) ?? throw new Exception("Platform not found");
	}

	public async Task<PlatformGetShortDto> GetPlatformShortAsync(long id)
	{
		Platform? entity = await _db.Platforms.FindAsync(id);
		if(entity == null)
			throw new Exception("Platform not found");
		return new PlatformGetShortDto(entity.Id, entity.Name);
	}

	public async Task<List<PlatformGetShortDto>> GetPlatformsShortAsync()
	{
		return await _db.Platforms.Select(x => new PlatformGetShortDto(x.Id, x.Name)).ToListAsync();
	}

	public async Task<bool> UpdatePlatformAsync(PlatformWriteDto dto)
	{
		Platform? entity = await _db.Platforms.FindAsync(dto.Id);
		if(entity == null)
			throw new Exception("Platform not found");
		entity.Description = dto.Description;
		entity.Name = dto.Name;
		entity.Updated = DateTime.UtcNow;
		return await _db.SaveChangesAsync() > 0;
	}
}

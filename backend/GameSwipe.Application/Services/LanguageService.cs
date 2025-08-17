
using GameSwipe.Application.Dtos.Languages;
using GameSwipe.Application.Interfaces.Services.Others;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Other;

using Microsoft.EntityFrameworkCore;

namespace GameSwipe.Application.Services;
public class LanguageService : ILanguageService
{
	private readonly GameSwipeDbContext _db;

	public LanguageService(GameSwipeDbContext db)
	{
		_db = db;
	}

	public async Task<bool> CreateLanguageAsync(LanguageWriteDto dto)
	{
		Language entity = new()
		{
			Created = DateTime.UtcNow,
			Name = dto.Name,
			Updated = DateTime.UtcNow,
		};
		_db.Languages.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteLanguageAsync(long id)
	{
		Language? entity = await _db.Languages.FindAsync(id);
		if(entity == null)
			throw new Exception("Language not found");
		_db.Languages.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<List<LanguageGetShortDto>> GetLanguagesShortAsync()
	{
		return await _db.Languages.Select(x => new LanguageGetShortDto(x.Id, x.Name)).ToListAsync();
	}

	public async Task<Language> GetLanguageBareAsync(long id)
	{
		return await _db.Languages.FindAsync(id) ?? throw new Exception("Language not found");
	}

	public async Task<LanguageGetShortDto> GetLanguageShortAsync(long id)
	{
		Language? entity = await _db.Languages.FindAsync(id);
		if(entity == null)
			throw new Exception("Language not found");
		return new LanguageGetShortDto(entity.Id, entity.Name);
	}

	public async Task<bool> UpdateLanguageAsync(LanguageWriteDto dto)
	{
		Language? entity = await _db.Languages.FindAsync(dto.Id);
		if(entity == null)
			throw new Exception("Language not found");
		entity.Name = dto.Name;
		entity.Updated = DateTime.UtcNow;
		return await _db.SaveChangesAsync() > 0;
	}
}

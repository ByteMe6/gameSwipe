using GameSwipe.Application.Dtos.Languages;
using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Interfaces.Services.Others;

public interface ILanguageService
{
	public Task<LanguageGetShortDto> GetLanguageShortAsync(long id);

	public Task<Language> GetLanguageBareAsync(long id);

	public Task<bool> CreateLanguageAsync(LanguageWriteDto dto);

	public Task<bool> UpdateLanguageAsync(LanguageWriteDto dto);

	public Task<bool> DeleteLanguageAsync(long id);

	public Task<List<LanguageGetShortDto>> GetLanguagesShortAsync();
}

using GameSwipe.Application.Dtos.General;
using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Dtos.Languages;

public class LanguageGetShortDto(long id, string name) : GetShortDto(id, name)
{
	public static explicit operator LanguageGetShortDto(Language language) => new(language.Id, language.Name);
}

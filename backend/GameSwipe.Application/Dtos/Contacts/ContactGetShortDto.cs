using GameSwipe.Application.Dtos.General;
using GameSwipe.Application.Dtos.Platforms;

namespace GameSwipe.Application.Dtos.Contacts;

public class ContactGetShortDto : GetShortDto
{
	public string Identificator { get; set; } = string.Empty;

	public PlatformGetShortDto Platform { get; set; } = null!;

	public ContactGetShortDto(long id, string name, string identificator, PlatformGetShortDto platform) : base(id, name)
	{
		Identificator = identificator;
		Platform = platform;
	}
}

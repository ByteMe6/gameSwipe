using GameSwipe.Application.Dtos.General;
using GameSwipe.Application.Dtos.Platforms;
using GameSwipe.DataAccess.Entities.Other;

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

	public static explicit operator ContactGetShortDto(Contact contact) => new(contact.Id, contact.Name, contact.Identificator, (PlatformGetShortDto)contact.Platform);
}

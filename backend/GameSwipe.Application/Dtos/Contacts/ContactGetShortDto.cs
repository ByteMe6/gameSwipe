using GameSwipe.Application.Dtos.Platforms;

namespace GameSwipe.Application.Dtos.Contacts;

public class ContactGetShortDto
{
	public long Id { get; set; }
	public string Identificator { get; set; } = string.Empty;
	public string? Name { get; set; }

	public PlatformGetShortDto Platform { get; set; } = new();
}

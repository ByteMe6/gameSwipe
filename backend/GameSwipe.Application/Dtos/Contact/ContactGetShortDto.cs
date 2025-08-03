using GameSwipe.Application.Dtos.Platform;

namespace GameSwipe.Application.Dtos.Contact;

public class ContactGetShortDto
{
	public long Id { get; set; }
	public string Identificator { get; set; } = string.Empty;
	public string? Name { get; set; }

	public PlatformGetShortDto Platform { get; set; } = new();
}

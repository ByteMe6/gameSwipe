using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Contacts;

public class ContactWriteDto
{
	public long? Id { get; set; }

	[Length(1, 50)]
	public string Identificator { get; set; }
	public string? Name { get; set; }

	[Required]
	public long PlatformId { get; set; }

	public ContactWriteDto(string identificator, long platformId
		, string? name = null, long? id = null)
	{
		Id = id;
		Identificator = identificator;
		Name = name;
		PlatformId = platformId;
	}
}

using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Platform;

public class PlatformWriteDto
{
	public long? Id { get; set; }

	[Length(2, 50)]
	public string Name { get; set; }

	[Length(5, 150)]
	public string Description { get; set; }

	public PlatformWriteDto(string name, string description, long? id = null)
	{
		Id = id;
		Name = name;
		Description = description;
	}
}

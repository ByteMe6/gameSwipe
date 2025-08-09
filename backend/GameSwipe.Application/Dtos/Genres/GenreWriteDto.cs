using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Genres;

public class GenreWriteDto
{
	public long? Id { get; set; }
	[Length(3, 50)]
	public string Name { get; set; }
	[Length(3, 150)]
	public string Description { get; set; }

	public GenreWriteDto(string name, string description, long? id = null)
	{
		Id = id;
		Name = name;
		Description = description;
	}
}

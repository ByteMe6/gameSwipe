using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Game;

public class GameWriteDto
{
	public long? Id { get; set; }
	[Length(3, 50)]
	public string Name { get; set; } = null!;
	[Length(3, 150)]
	public string Description { get; set; } = null!;
	[Required]
	public List<long> Genres { get; set; } = null!;

	public GameWriteDto(string name, string description, List<long> genres, long? id = null)
	{
		Id = id;
		Name = name;
		Description = description;
		Genres = genres;
	}
}

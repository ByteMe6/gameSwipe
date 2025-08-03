using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Language;

public class LanguageWriteDto
{
	public long? Id { get; set; }

	[Length(3, 30)]
	public string Name { get; set; }

	public LanguageWriteDto(string name, long? id = null)
	{
		Id = id;
		Name = name;
	}
}

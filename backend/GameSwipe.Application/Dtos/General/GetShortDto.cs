namespace GameSwipe.Application.Dtos.General;

public class GetShortDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public GetShortDto(long id, string name)
	{
		Id = id;
		Name = name;
	}
}

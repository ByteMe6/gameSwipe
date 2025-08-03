namespace GameSwipe.DataAccess.Entities.Games;


public class Genre : AuditableEntity
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public List<Game> Games { get; set; } = null!;
}


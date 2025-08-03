using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.DataAccess.Entities.Games;

public class GameRecord : AuditableEntity
{
	public User User { get; set; } = null!;
	public Game Game { get; set; } = null!;

	public int Playtime { get; set; }
	public string? Progress { get; set; }
}

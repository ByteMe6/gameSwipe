namespace GameSwipe.DataAccess.Entities.Users;

public class Match : AuditableEntity
{
	public long UserId { get; set; }
	public User User { get; set; } = null!;

	public long TargetUserId { get; set; }
	public User TargetUser { get; set; } = null!;

	public MatchStatus Status { get; set; }
}

public enum MatchStatus
{
	Seen = 0,
	Accept = 1,
	Decline = 2
}
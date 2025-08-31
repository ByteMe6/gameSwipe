using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.DataAccess.Entities.Other;

public class Contact : AuditableEntity
{
	public User User { get; set; } = null!;
	public Platform Platform { get; set; } = null!;

	public string Identificator { get; set; } = string.Empty;
	public string? Name { get; set; }
}

namespace GameSwipe.DataAccess.Entities.Other;

public class Contact : AuditableEntity
{
	public Platform Platform { get; set; } = null!;

	public string Identificator { get; set; } = string.Empty;
	public string? Name { get; set; }
}

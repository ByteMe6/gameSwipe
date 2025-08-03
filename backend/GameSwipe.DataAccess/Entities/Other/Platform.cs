namespace GameSwipe.DataAccess.Entities.Other;

public class Platform : AuditableEntity
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}


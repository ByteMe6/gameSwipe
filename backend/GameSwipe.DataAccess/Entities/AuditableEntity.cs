namespace GameSwipe.DataAccess.Entities;

public class AuditableEntity
{
	public long Id { get; set; }
	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }
}

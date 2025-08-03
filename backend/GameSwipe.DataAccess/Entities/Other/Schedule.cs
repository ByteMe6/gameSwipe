namespace GameSwipe.DataAccess.Entities.Other;

public class Schedule : AuditableEntity
{
	public WeekDays Day { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
}

public enum WeekDays
{
	Monday = 1,
	Tuesday = 2,
	Wednesday = 3,
	Thursday = 4,
	Friday = 5,
	Saturday = 6,
	Sunday = 7,
	WorkDay = 10,
	Weekend = 11,
}

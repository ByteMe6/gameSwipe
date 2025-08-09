using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Dtos.Schedules;

public class ScheduleWriteDto
{
	public long? Id { get; set; }

	public WeekDays Day { get; set; }

	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
}

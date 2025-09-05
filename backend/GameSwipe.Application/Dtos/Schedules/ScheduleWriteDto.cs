using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Dtos.Schedules;

public class ScheduleWriteDto
{
	public WeekDays Day { get; set; }

	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }

	public ScheduleWriteDto(WeekDays day, TimeOnly startTime, TimeOnly endTime)
	{
		Day = day;
		StartTime = startTime;
		EndTime = endTime;
	}
}

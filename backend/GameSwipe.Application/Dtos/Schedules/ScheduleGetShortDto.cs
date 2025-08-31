using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Dtos.Schedules;

public class ScheduleGetShortDto
{
	public long Id { get; set; }
	public WeekDays Day { get; set; }

	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }

	public static explicit operator ScheduleGetShortDto(Schedule schedule) => new()
	{
		Day = schedule.Day,
		EndTime = schedule.EndTime,
		Id = schedule.Id,
		StartTime = schedule.StartTime,
	};
}

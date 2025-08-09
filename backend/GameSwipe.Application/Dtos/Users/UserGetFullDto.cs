using GameSwipe.Application.Dtos.Contacts;
using GameSwipe.Application.Dtos.GameRecords;
using GameSwipe.Application.Dtos.Genres;
using GameSwipe.Application.Dtos.Languages;
using GameSwipe.Application.Dtos.Matches;
using GameSwipe.Application.Dtos.Schedules;

namespace GameSwipe.Application.Dtos.Users;

public class UserGetFullDto
{
	public long Id { get; set; }

	public DateTime Created { get; set; }
	public DateTime Updated { get; set; }

	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public DateTime BirthDate { get; set; }

	public string? Avatar { get; set; }

	public string Description { get; set; } = string.Empty;

	public string Location { get; set; } = string.Empty;

	public List<LanguageGetShortDto> Languages { get; set; } = new();

	public List<GenreGetShortDto> Genres { get; set; } = new();

	public string RolePreferred { get; set; } = string.Empty;

	public List<ScheduleGetShortDto> AvailableSchedules { get; set; } = new();

	public List<GameRecordGetShortDto> GameRecords { get; set; } = new();

	public string Preferences { get; set; } = string.Empty;

	public List<ContactGetShortDto> Contacts { get; set; } = new();

	public List<MatchGetShortDto> OwnMatches { get; set; } = new();
}

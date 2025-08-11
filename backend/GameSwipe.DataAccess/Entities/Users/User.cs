using GameSwipe.DataAccess.Entities.Games;
using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.DataAccess.Entities.Users;

public class User : AuditableEntity
{
	public string Username { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Name { get; set; } = null!;

	public string PasswordHash { get; set; } = null!;

	public bool Admin { get; set; }

	public DateTime BirthDate { get; set; }

	public string? Avatar { get; set; }

	public string Description { get; set; } = string.Empty;

	public string Location { get; set; } = string.Empty;

	public List<Language> Languages { get; set; } = null!;

	public List<Genre> Genres { get; set; } = null!;

	public string RolePreferred { get; set; } = string.Empty;

	public List<Schedule> AvailableSchedules { get; set; } = null!;

	public List<GameRecord> GameRecords { get; set; } = null!;

	public string Preferences { get; set; } = string.Empty;

	public List<Contact> Contacts { get; set; } = null!;

	public List<Match> OwnMatches { get; set; } = null!;

	public List<Match> ReceivedMatches { get; set; } = null!;
}

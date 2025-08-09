using GameSwipe.DataAccess.Entities.Users;

namespace GameSwipe.Application.Dtos.Users;

public class UserSearchDto
{
	public int? MinimumAge { get; set; }
	public int? MaximumAge { get; set; }

	public string? Location { get; set; }

	public List<long>? LanguageIds { get; set; }

	public List<long>? GenreIds { get; set; }

	public string? Role { get; set; }

	public string? Preferences { get; set; }

	public List<long>? PlatformIds { get; set; }

	public MatchStatus? Status { get; set; }
}

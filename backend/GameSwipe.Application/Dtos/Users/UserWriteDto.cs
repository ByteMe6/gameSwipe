using System.ComponentModel.DataAnnotations;

using GameSwipe.Application.Dtos.Contacts;
using GameSwipe.Application.Dtos.GameRecords;
using GameSwipe.Application.Dtos.Schedules;

namespace GameSwipe.Application.Dtos.Users;

public class UserWriteDto
{
	public long? Id { get; set; }
	[Length(3, 30)]
	public string Username { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	[Length(2, 70)]
	public string Name { get; set; }

	public string? Password { get; set; }

	[Required]
	public DateTime BirthDate { get; set; }

	public string? Avatar { get; set; }

	[Length(5, 150)]
	public string Description { get; set; }

	[Length(5, 150)]
	public string Location { get; set; }

	[Required]
	public List<long> LanguageIds { get; set; }

	[Required]
	public List<long> GenreIds { get; set; }

	[Required]
	public string RolePreferred { get; set; }

	[Required]
	public List<ScheduleWriteDto> AvailableSchedules { get; set; }

	[Required]
	public List<GameRecordWriteDto> GameRecords { get; set; }

	[Length(5, 100)]
	public string Preferences { get; set; }

	[Required]
	public List<ContactWriteDto> Contacts { get; set; }

	public UserWriteDto(long? id, string username, string email, string name, string? password, DateTime birthDate, string? avatar, string description, string location, List<long> languageIds, List<long> genreIds, string rolePreferred, List<ScheduleWriteDto> availableSchedules, List<GameRecordWriteDto> gameRecords, string preferences, List<ContactWriteDto> contacts)
	{
		Id = id;
		Username = username;
		Email = email;
		Name = name;
		Password = password;
		BirthDate = birthDate;
		Avatar = avatar;
		Description = description;
		Location = location;
		LanguageIds = languageIds;
		GenreIds = genreIds;
		RolePreferred = rolePreferred;
		AvailableSchedules = availableSchedules;
		GameRecords = gameRecords;
		Preferences = preferences;
		Contacts = contacts;
	}
}

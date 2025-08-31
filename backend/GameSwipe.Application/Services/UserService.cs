using GameSwipe.Application.Dtos.Users;
using GameSwipe.Application.Interfaces.Services;
using GameSwipe.Application.Interfaces.Services.Users;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Games;
using GameSwipe.DataAccess.Entities.Other;
using GameSwipe.DataAccess.Entities.Users;
using static BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using GameSwipe.Application.Dtos.Schedules;
using GameSwipe.Application.Dtos.Contacts;
using GameSwipe.Application.Dtos.GameRecords;
using GameSwipe.Application.Dtos.Genres;
using GameSwipe.Application.Dtos.Languages;
using GameSwipe.Application.Dtos.Matches;

namespace GameSwipe.Application.Services;

public class UserService : IUserService
{
	private readonly IIdentityService _id;
	private readonly GameSwipeDbContext _db;

	public UserService(GameSwipeDbContext db, IIdentityService id)
	{
		_db = db;
		_id = id;
	}

	public async Task<bool> CreateUserAsync(UserWriteDto dto)
	{
		User? usernameEntity = await _db.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
		if(usernameEntity is not null)
			throw new Exception("Username is already taken");

		User? emailEntity = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
		if(emailEntity is not null)
			throw new Exception("Email is already taken");

		if(dto.Password is null)
			throw new Exception("Password is needed");

		if(await _db.Languages.CountAsync(x => dto.LanguageIds.Contains(x.Id)) != dto.LanguageIds.Count)
			throw new Exception("Some languages are not found");

		if(await _db.Genres.CountAsync(x => dto.GenreIds.Contains(x.Id)) != dto.GenreIds.Count)
			throw new Exception("Some genres are not found");

		List<Platform> platforms = await _db.Platforms.Where(x => dto.Contacts.Select(x => x.PlatformId).Contains(x.Id)).ToListAsync();
		if(platforms.Count != dto.Contacts.Count)
			throw new Exception("Some platforms are not found");

		List<Game> games = await _db.Games.Where(x => dto.GameRecords.Select(x => x.GameId).Contains(x.Id)).ToListAsync();

		User entity = new User()
		{
			Avatar = dto.Avatar,
			Created = DateTime.UtcNow,
			Email = dto.Email,
			Languages = await _db.Languages.Where(x => dto.LanguageIds.Contains(x.Id)).ToListAsync(),
			Name = dto.Name,
			PasswordHash = HashPassword(dto.Password),
			Username = dto.Username,
			Genres = await _db.Genres.Where(x => dto.GenreIds.Contains(x.Id)).ToListAsync(),
			Updated = DateTime.UtcNow,
			Admin = false,
			BirthDate = dto.BirthDate,
			Description = dto.Description,
			Location = dto.Location,
			Preferences = dto.Preferences,
			RolePreferred = dto.RolePreferred,
			AvailableSchedules = dto.AvailableSchedules.Select(
				x => new Schedule()
				{
					Created = DateTime.UtcNow,
					Day = x.Day,
					EndTime = x.EndTime,
					StartTime = x.StartTime,
					Updated = DateTime.UtcNow,
				}).ToList(),
			Contacts = dto.Contacts.Select(
				x => new Contact()
				{
					Created = DateTime.UtcNow,
					Identificator = x.Identificator,
					Name = x.Name,
					Platform = platforms.Find(y => y.Id == x.PlatformId)!,
					Updated = DateTime.UtcNow,
				}).ToList(),
			GameRecords = dto.GameRecords.Select(
				x => new GameRecord()
				{
					Created = DateTime.UtcNow,
					Game = games.Find(y => y.Id == x.GameId)!,
					Playtime = x.Playtime,
					Progress = x.Progress,
					Updated = DateTime.UtcNow,
				}).ToList(),
		};
		_db.Users.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteUserAsync(long? id)
	{
		if(id is null)
			id = _id.Id;

		if(id != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		User? entity = _db.Users.Find(id);
		if(entity is null)
			throw new Exception("User not found");

		_db.Users.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<UserGetFullDto> GetUserFullAsync(long? id)
	{
		if(id is null)
			id = _id.Id;

		bool matched = await _db.Matches.AnyAsync(x => x.UserId == id && x.TargetUserId == _id.Id && x.Status == MatchStatus.Accept);

		if(id != _id.Id && !_id.IsAdmin && !matched)
			throw new Exception("Access denied");

		User? entity = await _db.Users
			.Include(x => x.Languages)
			.Include(x => x.Genres)
			.Include(x=>x.OwnMatches).ThenInclude(x=>x.TargetUser)
			.Include(x=>x.OwnMatches).ThenInclude(x=>x.User)
			.Include(x=>x.AvailableSchedules)
			.Include(x=>x.Contacts).ThenInclude(x=>x.Platform)
			.Include(x=>x.GameRecords).ThenInclude(x=>x.Game)
			.Include(x=>x.GameRecords).ThenInclude(x=>x.User)
			.FirstOrDefaultAsync(x => x.Id == id);

		if(entity is null)
			throw new Exception("User not found");

		UserGetFullDto dto = new UserGetFullDto()
		{
			AvailableSchedules = entity.AvailableSchedules.Select(x => (ScheduleGetShortDto)x).ToList(),
			Avatar = entity.Avatar,
			BirthDate = entity.BirthDate,
			Contacts = entity.Contacts.Select(x => (ContactGetShortDto)x).ToList(),
			Created = entity.Created,
			Description = entity.Description,
			Email = entity.Email,
			GameRecords = entity.GameRecords.Select(x => (GameRecordGetShortDto)x).ToList(),
			Genres = entity.Genres.Select(x => (GenreGetShortDto)x).ToList(),
			Id = entity.Id,
			Languages = entity.Languages.Select(x => (LanguageGetShortDto)x).ToList(),
			Location = entity.Location,
			Name = entity.Name,
			OwnMatches = entity.OwnMatches.Select(x => (MatchGetShortDto)x).ToList(),
			Preferences = entity.Preferences,
			RolePreferred = entity.RolePreferred,
			Updated = entity.Updated,
			Username = entity.Username,
		};
		return dto;
	}

	public Task<List<UserGetShortDto>> GetUsersByIdsAsync(List<long> ids)
	{
		throw new NotImplementedException();
	}

	public Task<string> LoginAsync(UserLoginDto dto)
	{
		throw new NotImplementedException();
	}

	public Task<bool> ResetPasswordAsync(string email)
	{
		throw new NotImplementedException();
	}

	public Task<List<UserGetShortDto>> SearchUsersAsync(UserSearchDto dto, int page = 1, int pageSize = 20)
	{
		throw new NotImplementedException();
	}

	public Task<bool> SendEmailVerificationAsync(string email)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateUserAsync(UserWriteDto dto)
	{
		throw new NotImplementedException();
	}

	public Task<bool> VerifyEmailAsync(string email, string code)
	{
		throw new NotImplementedException();
	}
}

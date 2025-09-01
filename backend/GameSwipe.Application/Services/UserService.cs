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
	private readonly ITokenService _token;
	private readonly GameSwipeDbContext _db;

	public UserService(GameSwipeDbContext db, IIdentityService id, ITokenService token)
	{
		_db = db;
		_id = id;
		_token = token;
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

		List<Language> languages = await _db.Languages.Where(x => dto.LanguageIds.Contains(x.Id)).ToListAsync();
		if(languages.Count != dto.LanguageIds.Count)
			throw new Exception("Some languages are not found");

		List<Genre> genres = await _db.Genres.Where(x => dto.GenreIds.Contains(x.Id)).ToListAsync();
		if(genres.Count != dto.GenreIds.Count)
			throw new Exception("Some genres are not found");

		List<Platform> platforms = await _db.Platforms.Where(x => dto.Contacts.Select(x => x.PlatformId).Contains(x.Id)).ToListAsync();
		if(platforms.Count != dto.Contacts.Count)
			throw new Exception("Some platforms are not found");

		List<Game> games = await _db.Games.Where(x => dto.GameRecords.Select(x => x.GameId).Contains(x.Id)).ToListAsync();
		if(games.Count != dto.GameRecords.Count)
			throw new Exception("Some games are not found");

		User entity = new User()
		{
			Avatar = dto.Avatar,
			Created = DateTime.UtcNow,
			Email = dto.Email,
			Languages = languages,
			Name = dto.Name,
			PasswordHash = HashPassword(dto.Password),
			Username = dto.Username,
			Genres = genres,
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

	public async Task<UserWriteDto> GetTemplateAsync()
	{
		//to finish
		User? entity = await _db.Users
			.Include(x => x.Languages)
			.Include(x => x.Genres)
			.Include(x => x.OwnMatches).ThenInclude(x => x.TargetUser)
			.Include(x => x.OwnMatches).ThenInclude(x => x.User)
			.Include(x => x.AvailableSchedules)
			.Include(x => x.Contacts).ThenInclude(x => x.Platform)
			.Include(x => x.GameRecords).ThenInclude(x => x.Game)
			.Include(x => x.GameRecords).ThenInclude(x => x.User)
			.FirstOrDefaultAsync(x => x.Id == _id.Id);

		if(entity is null)
			throw new Exception("User not found");

		List<ScheduleWriteDto> schedules = entity.AvailableSchedules.Select(x => new ScheduleWriteDto(x.Day, x.StartTime, x.EndTime)).ToList();

		UserWriteDto dto = new UserWriteDto(_id.Id, entity.Username, entity.Email, entity.Name, null, entity.BirthDate, entity.Avatar, entity.Description, entity.Location, entity.Languages.Select(x => x.Id).ToList(), entity.Genres.Select(x => x.Id).ToList(), entity.RolePreferred, .., .., entity.Preferences, ..);
		return dto;
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
			.Include(x => x.OwnMatches).ThenInclude(x => x.TargetUser)
			.Include(x => x.OwnMatches).ThenInclude(x => x.User)
			.Include(x => x.AvailableSchedules)
			.Include(x => x.Contacts).ThenInclude(x => x.Platform)
			.Include(x => x.GameRecords).ThenInclude(x => x.Game)
			.Include(x => x.GameRecords).ThenInclude(x => x.User)
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

	public async Task<List<UserGetShortDto>> GetUsersByIdsAsync(List<long> ids)
	{
		if(ids.Count > 50)
			throw new Exception("Too many ids");
		ids = ids.Distinct().ToList();
		bool matched = await _db.Matches.CountAsync(x => x.TargetUserId == _id.Id && ids.Contains(x.UserId) && x.Status == MatchStatus.Accept) + (ids.Contains(_id.Id) ? 1 : 0) == ids.Count;

		if(!_id.IsAdmin && !matched)
			throw new Exception("Access denied");

		return await _db.Users.Where(x => ids.Contains(x.Id)).Select(x => (UserGetShortDto)x).ToListAsync();
	}

	public async Task<string> LoginAsync(UserLoginDto dto)
	{
		User? user = null;
		if(dto.Type == IdentificationType.Email)
			user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Identificator);
		if(dto.Type == IdentificationType.Username)
			user = await _db.Users.FirstOrDefaultAsync(x => x.Username == dto.Identificator);

		if(user is null)
			throw new Exception("User not found");

		if(!Verify(dto.Password, user.PasswordHash))
			throw new Exception("Wrong password");

		return _token.GenerateToken(user);
	}

	public async Task<string> ReloginAsync(string token)
	{
		User? user = await _db.Users.FindAsync(_token.GetId(token));
		if(user is null)
			throw new Exception("User not found");

		if(user.Updated > _token.GetIssueDate(token))
			throw new Exception("Token is expired");

		if(!_token.IsValid(token))
			throw new Exception("Token is invalid");

		return _token.GenerateToken(user);
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

	public async Task<bool> UpdateUserAsync(UserWriteDto dto)
	{
		if(dto.Id is null)
			throw new Exception("Id is null");

		User? entity = await _db.Users.FindAsync(dto.Id);
		if(entity is null)
			throw new Exception("User not found");

		User? usernameEntity = await _db.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
		if(usernameEntity is not null && usernameEntity.Id != entity.Id)
			throw new Exception("Username is already taken");

		User? emailEntity = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
		if(emailEntity is not null && emailEntity.Id != entity.Id)
			throw new Exception("Email is already taken");

		List<Language> languages = await _db.Languages.Where(x => dto.LanguageIds.Contains(x.Id)).ToListAsync();
		if(languages.Count != dto.LanguageIds.Count)
			throw new Exception("Some languages are not found");

		List<Genre> genres = await _db.Genres.Where(x => dto.GenreIds.Contains(x.Id)).ToListAsync();
		if(genres.Count != dto.GenreIds.Count)
			throw new Exception("Some genres are not found");

		List<Platform> platforms = await _db.Platforms.Where(x => dto.Contacts.Select(x => x.PlatformId).Contains(x.Id)).ToListAsync();
		if(platforms.Count != dto.Contacts.Count)
			throw new Exception("Some platforms are not found");

		List<Game> games = await _db.Games.Where(x => dto.GameRecords.Select(x => x.GameId).Contains(x.Id)).ToListAsync();
		if(games.Count != dto.GameRecords.Count)
			throw new Exception("Some games are not found");

		if(dto.Password is not null)
		{
			if(dto.Password.Length < 8 || dto.Password.Length > 32)
				throw new Exception("Password must be between 8 and 32 characters");

			entity.PasswordHash = HashPassword(dto.Password);
		}

		entity.Avatar = dto.Avatar;
		entity.Created = DateTime.UtcNow;
		entity.Email = dto.Email;
		entity.Languages = languages;
		entity.Name = dto.Name;
		entity.Username = dto.Username;
		entity.Genres = genres;
		entity.Updated = DateTime.UtcNow;
		entity.Admin = false;
		entity.BirthDate = dto.BirthDate;
		entity.Description = dto.Description;
		entity.Location = dto.Location;
		entity.Preferences = dto.Preferences;
		entity.RolePreferred = dto.RolePreferred;
		entity.AvailableSchedules = dto.AvailableSchedules.Select(
			x => new Schedule()
			{
				Created = DateTime.UtcNow,
				Day = x.Day,
				EndTime = x.EndTime,
				StartTime = x.StartTime,
				Updated = DateTime.UtcNow,
			}).ToList();
		entity.Contacts = dto.Contacts.Select(
			x => new Contact()
			{
				Created = DateTime.UtcNow,
				Identificator = x.Identificator,
				Name = x.Name,
				Platform = platforms.Find(y => y.Id == x.PlatformId)!,
				Updated = DateTime.UtcNow,
			}).ToList();
		entity.GameRecords = dto.GameRecords.Select(
			x => new GameRecord()
			{
				Created = DateTime.UtcNow,
				Game = games.Find(y => y.Id == x.GameId)!,
				Playtime = x.Playtime,
				Progress = x.Progress,
				Updated = DateTime.UtcNow,
			}).ToList();
		_db.Users.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public Task<bool> VerifyEmailAsync(string email, string code)
	{
		throw new NotImplementedException();
	}
}

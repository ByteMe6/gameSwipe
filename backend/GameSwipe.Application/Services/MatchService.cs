
using GameSwipe.Application.Dtos.Matches;
using GameSwipe.Application.Dtos.Users;
using GameSwipe.Application.Interfaces.Services;
using GameSwipe.Application.Interfaces.Services.Users;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Users;

using Microsoft.EntityFrameworkCore;


namespace GameSwipe.Application.Services;

public class MatchService : IMatchService
{
	private readonly GameSwipeDbContext _db;
	private readonly IIdentityService _id;

	public MatchService(GameSwipeDbContext db, IIdentityService id)
	{
		_db = db;
		_id = id;
	}

	public async Task<bool> CreateMatchAsync(MatchWriteDto dto)
	{
		if(dto.TargetUserId is null)
			throw new Exception("Target user id is null");
		User? target = await _db.Users.FindAsync(dto.TargetUserId);
		if(target is null)
			throw new Exception("Target not found");

		Match entity = new()
		{
			Status = dto.Status,
			Created = DateTime.UtcNow,
			TargetUserId = dto.TargetUserId ?? throw new Exception("Something miraculous happened"),
			Updated = DateTime.UtcNow,
			UserId = _id.Id
		};

		_db.Matches.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteMatchAsync(long id)
	{
		Match? entity = await _db.Matches.FindAsync(id);
		if(entity is null)
			throw new Exception("Match not found");

		if(entity.UserId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		_db.Matches.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<List<MatchGetShortDto>> GetMatchesAcceptedAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches.Include(x => x.TargetUser).Include(x => x.User)
			.Where(x => x.UserId == userId)
			.Where(x => x.Status == MatchStatus.Accept)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesAllAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches.Include(x => x.TargetUser).Include(x => x.User)
			.Where(x => x.UserId == userId || x.TargetUserId == userId)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesDeclinedAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches.Include(x => x.TargetUser).Include(x => x.User)
			.Where(x => x.UserId == userId)
			.Where(x => x.Status == MatchStatus.Decline)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesMutualAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		var result = from ew in _db.Matches
					 join sdf in _db.Users
					 on ew.UserId equals sdf.Id
					 join fio in _db.Users
					 on ew.TargetUserId equals fio.Id
					 select new
					 {
						 ew.UserId,
						 ew.Id,
						 sdf.Name
					 };
		var list =  await result.ToListAsync();

		return await _db.Matches.Include(x => x.TargetUser).Include(x => x.User)
			.Join(
				_db.Matches,
				m1 => new { UserId = m1.UserId, TargetUserId = m1.TargetUserId },
				m2 => new { UserId = m2.TargetUserId, TargetUserId = m2.UserId },
				(m1, m2) => new { m1, m2 }
			)
			.Where(x => x.m1.UserId == userId &&
						x.m1.Status == MatchStatus.Accept &&
						x.m2.Status == MatchStatus.Accept)
			.Select(x => x.m1)
			.Skip(skip)
			.Take(pageSize)
			.Select(m => new MatchGetShortDto(
				m.Id,
				new UserGetShortDto(m.User.Id, m.User.Username, m.User.Name, m.User.Avatar),
				new UserGetShortDto(m.TargetUser.Id, m.TargetUser.Username, m.TargetUser.Name, m.TargetUser.Avatar)
			))
			.ToListAsync();
	}


	public async Task<List<MatchGetShortDto>> GetMatchesReceivedAsync(long? userId, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches.Include(x => x.TargetUser).Include(x => x.User).Where(x => x.TargetUserId == _id.Id && x.Status == MatchStatus.Accept).Select(
			m => new MatchGetShortDto(
				m.Id,
				new UserGetShortDto(m.User.Id, m.User.Username, m.User.Name, m.User.Avatar),
				new UserGetShortDto(m.TargetUser.Id, m.TargetUser.Username, m.TargetUser.Name, m.TargetUser.Avatar)
			)).Skip(skip).Take(pageSize).ToListAsync();
	}

	public async Task<bool> UpdateMatchAsync(MatchWriteDto dto)
	{
		Match? entity = await _db.Matches.FindAsync(dto.Id);
		if(entity == null)
			throw new Exception("Match not found");

		entity.Status = dto.Status;
		entity.Updated = DateTime.UtcNow;
		return await _db.SaveChangesAsync() > 0;
	}
}
